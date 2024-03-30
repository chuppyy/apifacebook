using System;
using System.Collections.Generic;
using System.Text;

namespace ITC.Domain.Extensions;

public class ActionHelper
{
    /// <summary>
    ///     Sinh câu lệnh sql để xóa dữ liệu sử dụng left - right và chuyển dữ liệu con qua nhánh mới
    /// </summary>
    /// <param name="idOld">id dữ liệu cần xóa</param>
    /// <param name="leftOld">left của dữ liệu cần xóa</param>
    /// <param name="rightOld">right của dữ liệu cần xóa</param>
    /// <param name="idNew">id mới để chuyển dữ liệu con qua</param>
    /// <param name="management">managementId</param>
    /// <param name="isUseType">true: nếu câu lệnh có dùng Type các kiểu dữ liệu - mặc định false</param>
    /// <param name="typeId">giá trị typeId</param>
    /// <returns></returns>
    public StringBuilder GeneralDeleteWithLeftRightSql(string tableName, string idOld, int leftOld, int rightOld,
                                                       string idNew,
                                                       string management, bool isUseType, int typeId)
    {
        var sbBuilder = new StringBuilder();
        //--------------------1. Lấy dữ liệu chính nó và con của nó--------------------------------
        sbBuilder.Append("DECLARE @childTable TABLE (childrenId INT) ");
        sbBuilder.Append("INSERT INTO @childTable(childrenId) ");
        sbBuilder.Append("SELECT Id FROM " + tableName + " a ");
        sbBuilder.Append("WHERE a.IsDeleted = 0 AND a.PLeft > " + (leftOld  - 1) +
                         " AND a.PRight < "                     + (rightOld + 1) +
                         " AND a.ManagementId = "               + management     + " ");
        sbBuilder.Append(isUseType ? "AND a.TypeId = " + typeId + " " : " ");
        //--------------------2. Cập nhật lại vị trí left - right bị ảnh hưởng--------------------------------
        sbBuilder.Append("UPDATE "                           + tableName                +
                         " SET PLeft = PLeft - "             + (rightOld - leftOld + 1) +
                         " WHERE IsDeleted = 0 AND PLeft > " + rightOld                 +
                         " AND ManagementId = "              + management               + " ");
        sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
        sbBuilder.Append("UPDATE "                            + tableName                +
                         " SET PRight = PRight - "            + (rightOld - leftOld + 1) +
                         " WHERE IsDeleted = 0 AND PRight > " + rightOld                 +
                         " AND ManagementId = "               + management               + " ");
        sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
        //--------------------3. Kiểm tra điều kiện có chọn cha mới hay không ?------------------------------
        if (!string.IsNullOrEmpty(idNew))
        {
            //-----------------Không có chọn cha mới---------------------------------------------------------
            sbBuilder.Append("    UPDATE " + tableName +
                             " SET IsDeleted = 1 where Id IN (SELECT childrenId FROM @childTable) ");
        }
        else
        {
            //--------------------3. Lấy dữ liệu cha mới--------------------------------
            sbBuilder.Append("DECLARE @fatherRight INT ");
            sbBuilder.Append("SET @fatherRight = (SELECT PRight FROM " + tableName  +
                             " WHERE Id = "                            + idNew      +
                             " AND ManagementId = "                    + management + " ");
            sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + ") " : " ) ");
            //--------------------4. Xử lý dữ liệu cha mới--------------------------------
            sbBuilder.Append("IF @fatherRight IS NOT NULL ");
            sbBuilder.Append("BEGIN ");
            //--------------------4.1 Cập nhật vị trí left cho các dữ liệu con theo dữ liệu cha mới--------------------------------
            sbBuilder.Append("       UPDATE " + tableName + " ");
            sbBuilder.Append("            SET PLeft  =  PLeft + (@fatherRight  - " + leftOld + ") - 1 ");
            sbBuilder.Append("                WHERE Id IN (SELECT childrenId FROM @childTable WHERE childrenId != " +
                             idOld + ") ");
            //--------------------4.2 Cập nhật vị trí right cho các dữ liệu con theo dữ liệu cha mới--------------------------------
            sbBuilder.Append("        UPDATE " + tableName + " ");
            sbBuilder.Append("            SET PRight =  PRight + (@fatherRight - " + leftOld + ") - 1 ");
            sbBuilder.Append("                WHERE Id IN (SELECT childrenId FROM @childTable WHERE childrenId != " +
                             idOld + ") ");
            //--------------------4.3 Cập nhật vị trí left cho các dữ liệu bị ảnh hưởng--------------------------------
            sbBuilder.Append("        UPDATE "                  + tableName                + " ");
            sbBuilder.Append("            SET PLeft = PLeft + " + (rightOld - leftOld - 1) + " ");
            sbBuilder.Append("                WHERE IsDeleted = 0 AND PLeft > @fatherRight ");
            sbBuilder.Append(
                "                    AND Id NOT IN (SELECT childrenId FROM @childTable) AND ManagementId = " +
                management                                                                                   + " ");
            sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
            //--------------------4.5 Cập nhật vị trí right cho các dữ liệu bị ảnh hưởng--------------------------------
            sbBuilder.Append("        UPDATE "                    + tableName                + " ");
            sbBuilder.Append("            SET PRight = PRight + " + (rightOld - leftOld - 1) + " ");
            sbBuilder.Append("                WHERE IsDeleted = 0 AND  PRight >= @fatherRight ");
            sbBuilder.Append(
                "                    AND Id NOT IN (SELECT childrenId FROM @childTable) AND ManagementId = " +
                management                                                                                   + " ");
            sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
            //--------------------4.6 Xóa dữ liệu yêu cầu xóa--------------------------------
            sbBuilder.Append("        UPDATE " + tableName + " SET IsDeleted = 1 where Id = " + idOld + " ");
            //--------------------4.7 Cập nhật lại Id cha cho các dữ liệu con--------------------------------
            sbBuilder.Append("        UPDATE " + tableName + " ");
            sbBuilder.Append("            SET ParentId = "                                           + idNew +
                             " WHERE Id IN (SELECT childrenId FROM @childTable WHERE childrenId != " +
                             idOld                                                                   + ") ");
            sbBuilder.Append("    END ");
            sbBuilder.Append("ELSE ");
            sbBuilder.Append("    UPDATE " + tableName +
                             " SET IsDeleted = 1 where Id IN (SELECT childrenId FROM @childTable) ");
        }

        return sbBuilder;
    }

    /// <summary>
    ///     Sinh câu lệnh sql để xóa dữ liệu sử dụng left - right và chuyển dữ liệu con qua nhánh mới
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="idOld"></param>
    /// <param name="leftOld"></param>
    /// <param name="rightOld"></param>
    /// <param name="idNew"></param>
    /// <param name="projectId"></param>
    /// <param name="isUseType"></param>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public StringBuilder GeneralDeleteWithLeftRightSqlWithProject(string tableName, string idOld, int leftOld,
                                                                  int    rightOld,  string idNew,
                                                                  Guid   projectId, bool   isUseType, int typeId)
    {
        var sbBuilder = new StringBuilder();
        //--------------------1. Lấy dữ liệu chính nó và con của nó--------------------------------
        sbBuilder.Append("DECLARE @childTable TABLE (childrenId INT) ");
        sbBuilder.Append("INSERT INTO @childTable(childrenId) ");
        sbBuilder.Append("SELECT Id FROM " + tableName + " a ");
        sbBuilder.Append("WHERE a.IsDeleted = 0 AND a.MLeft > " + (leftOld  - 1) +
                         " AND a.MRight < "                     + (rightOld + 1) +
                         " AND a.ProjectId = "                  + projectId      + " ");
        sbBuilder.Append(isUseType ? "AND a.TypeId = " + typeId + " " : " ");
        //--------------------2. Cập nhật lại vị trí left - right bị ảnh hưởng--------------------------------
        sbBuilder.Append("UPDATE "                           + tableName                +
                         " SET MLeft = MLeft - "             + (rightOld - leftOld + 1) +
                         " WHERE IsDeleted = 0 AND MLeft > " + rightOld                 +
                         " AND ProjectId = "                 + projectId                + " ");
        sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
        sbBuilder.Append("UPDATE "                            + tableName                +
                         " SET MRight = MRight - "            + (rightOld - leftOld + 1) +
                         " WHERE IsDeleted = 0 AND MRight > " + rightOld                 +
                         " AND ProjectId = "                  + projectId                + " ");
        sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
        //--------------------3. Kiểm tra điều kiện có chọn cha mới hay không ?------------------------------
        if (!string.IsNullOrEmpty(idNew))
        {
            //-----------------Không có chọn cha mới---------------------------------------------------------
            sbBuilder.Append("    UPDATE " + tableName +
                             " SET IsDeleted = 1 where Id IN (SELECT childrenId FROM @childTable) ");
        }
        else
        {
            //--------------------3. Lấy dữ liệu cha mới--------------------------------
            sbBuilder.Append("DECLARE @fatherRight INT ");
            sbBuilder.Append("SET @fatherRight = (SELECT MRight FROM " + tableName +
                             " WHERE Id = "                            + idNew     +
                             " AND ProjectId = "                       + projectId + " ");
            sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + ") " : " ) ");
            //--------------------4. Xử lý dữ liệu cha mới--------------------------------
            sbBuilder.Append("IF @fatherRight IS NOT NULL ");
            sbBuilder.Append("BEGIN ");
            //--------------------4.1 Cập nhật vị trí left cho các dữ liệu con theo dữ liệu cha mới--------------------------------
            sbBuilder.Append("       UPDATE " + tableName + " ");
            sbBuilder.Append("            SET MLeft  =  MLeft + (@fatherRight  - " + leftOld + ") - 1 ");
            sbBuilder.Append("                WHERE Id IN (SELECT childrenId FROM @childTable WHERE childrenId != " +
                             idOld + ") ");
            //--------------------4.2 Cập nhật vị trí right cho các dữ liệu con theo dữ liệu cha mới--------------------------------
            sbBuilder.Append("        UPDATE " + tableName + " ");
            sbBuilder.Append("            SET MRight =  MRight + (@fatherRight - " + leftOld + ") - 1 ");
            sbBuilder.Append("                WHERE Id IN (SELECT childrenId FROM @childTable WHERE childrenId != " +
                             idOld + ") ");
            //--------------------4.3 Cập nhật vị trí left cho các dữ liệu bị ảnh hưởng--------------------------------
            sbBuilder.Append("        UPDATE "                  + tableName                + " ");
            sbBuilder.Append("            SET MLeft = MLeft + " + (rightOld - leftOld - 1) + " ");
            sbBuilder.Append("                WHERE IsDeleted = 0 AND MLeft > @fatherRight ");
            sbBuilder.Append("                    AND Id NOT IN (SELECT childrenId FROM @childTable) AND ProjectId = " +
                             projectId + " ");
            sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
            //--------------------4.5 Cập nhật vị trí right cho các dữ liệu bị ảnh hưởng--------------------------------
            sbBuilder.Append("        UPDATE "                    + tableName                + " ");
            sbBuilder.Append("            SET MRight = MRight + " + (rightOld - leftOld - 1) + " ");
            sbBuilder.Append("                WHERE IsDeleted = 0 AND  MRight >= @fatherRight ");
            sbBuilder.Append("                    AND Id NOT IN (SELECT childrenId FROM @childTable) AND ProjectId = " +
                             projectId + " ");
            sbBuilder.Append(isUseType ? "AND TypeId = " + typeId + " " : " ");
            //--------------------4.6 Xóa dữ liệu yêu cầu xóa--------------------------------
            sbBuilder.Append("        UPDATE " + tableName + " SET IsDeleted = 1 where Id = " + idOld + " ");
            //--------------------4.7 Cập nhật lại Id cha cho các dữ liệu con--------------------------------
            sbBuilder.Append("        UPDATE " + tableName + " ");
            sbBuilder.Append("            SET ParentId = "                                           + idNew +
                             " WHERE Id IN (SELECT childrenId FROM @childTable WHERE childrenId != " +
                             idOld                                                                   + ") ");
            sbBuilder.Append("    END ");
            sbBuilder.Append("ELSE ");
            sbBuilder.Append("    UPDATE " + tableName +
                             " SET IsDeleted = 1 where Id IN (SELECT childrenId FROM @childTable) ");
        }

        return sbBuilder;
    }

    /// <summary>
    ///     Sinh câu lệnh sql search
    /// </summary>
    /// <param name="model">Danh sách các trường cần thực hiện tra cứu</param>
    /// <param name="value">Giá trị tra cứu</param>
    /// <param name="symbol">Ký tự đại diện cho table cần tra cứu</param>
    /// <returns></returns>
    public StringBuilder GeneralSqlSearch(List<string> model, string value, string symbol)
    {
        var sBuilder = new StringBuilder();
        if (!string.IsNullOrEmpty(value))
        {
            var stt = 0;
            foreach (var z in model)
            {
                sBuilder.Append(stt == 0 ? "AND (" : " OR ");
                sBuilder.Append(symbol + "." + z + " COLLATE Latin1_General_CI_AI LIKE N'%" + value + "%' ");
                stt++;
            }

            sBuilder.Append(") ");
        }
        else
        {
            sBuilder.Append(" ");
        }

        return sBuilder;
    }

    /// <summary>
    ///     Sinh câu lệnh sql phân trang
    /// </summary>
    /// <param name="pageSize">Số dòng hiển thị trên 1 trang</param>
    /// <param name="pageNumber">Số trang</param>
    /// <returns></returns>
    public StringBuilder GeneralSqlPaging(int pageSize, int? pageNumber)
    {
        var sBuilder = new StringBuilder();
        pageSize   = pageSize   > 4 ? pageSize : 5;
        pageNumber = pageNumber > 0 ? pageNumber : 1;
        sBuilder.Append("OFFSET "          + pageSize + "  *(" + pageNumber + " - 1) ");
        sBuilder.Append("ROWS FETCH NEXT " + pageSize + " ROWS ONLY  ");
        return sBuilder;
    }
}