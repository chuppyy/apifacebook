#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
///     HelperExtension
/// </summary>
public static class HelperExtension
{
#region Static Fields and Constants

    /// <summary>
    ///     PathImageAccount
    /// </summary>
    public const string PathImageAccount = "Uploads/Images";

    /// <summary>
    ///     PathUpload
    /// </summary>
    public const string PathUpload = "Uploads";

    private static readonly string[] mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');

#endregion

#region Methods

    /// <summary>
    /// </summary>
    /// <param name="so"></param>
    /// <returns></returns>
    public static string ChuyenSoSangChuoi(double so)
    {
        if (so == 0)
            return mNumText[0];

        string chuoi = "", hauto = "";
        long   ty;
        do
        {
            //Lấy số hàng tỷ
            ty = Convert.ToInt64(Math.Floor(so / 1000000000));
            //Lấy phần dư sau số hàng tỷ
            so = so % 1000000000;
            if (ty > 0)
                chuoi = DocHangTrieu(so, true) + hauto + chuoi;
            else
                chuoi = DocHangTrieu(so, false) + hauto + chuoi;
            hauto = " tỷ";
        } while (ty > 0);

        return chuoi + " đồng";
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext   = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    /// <summary>
    ///     GetMimeTypes
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            {
                ".txt", "text/plain"
            },
            {
                ".pdf", "application/pdf"
            },
            {
                ".doc", "application/vnd.ms-word"
            },
            {
                ".docx", "application/vnd.ms-word"
            },
            {
                ".xls", "application/vnd.ms-excel"
            },
            {
                ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            },
            {
                ".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"
            },
            {
                ".png", "image/png"
            },
            {
                ".jpg", "image/jpeg"
            },
            {
                ".jpeg", "image/jpeg"
            },
            {
                ".gif", "image/gif"
            },
            {
                ".csv", "text/csv"
            },
            {
                ".zip", "application/zip"
            }
        };
    }


    /// <summary>
    ///     Chuyển đổi số int sang chữ cái la mã
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ToRoman(int number)
    {
        if (number < 0 || number > 3999) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");

        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D"  + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C"  + ToRoman(number - 100);
        if (number >= 90) return "XC"  + ToRoman(number - 90);
        if (number >= 50) return "L"   + ToRoman(number - 50);
        if (number >= 40) return "XL"  + ToRoman(number - 40);
        if (number >= 10) return "X"   + ToRoman(number - 10);
        if (number >= 9) return "IX"   + ToRoman(number - 9);
        if (number >= 5) return "V"    + ToRoman(number - 5);
        if (number >= 4) return "IV"   + ToRoman(number - 4);
        if (number >= 1) return "I"    + ToRoman(number - 1);

        throw new ArgumentOutOfRangeException("something bad happened");
    }

    private static string DocHangChuc(double so, bool daydu)
    {
        var chuoi = "";
        //Hàm để lấy số hàng chục ví dụ 21/10 = 2
        var chuc = Convert.ToInt64(Math.Floor(so / 10));
        //Lấy số hàng đơn vị bằng phép chia 21 % 10 = 1
        var donvi = (long)so % 10;
        //Nếu số hàng chục tồn tại tức >=20
        if (chuc > 1)
        {
            chuoi = " " + mNumText[chuc] + " mươi";
            if (donvi == 1) chuoi += " mốt";
        }
        else if (chuc == 1)
        {
            //Số hàng chục từ 10-19
            chuoi = " mười";
            if (donvi == 1) chuoi += " một";
        }
        else if (daydu && donvi > 0)
        {
            //Nếu hàng đơn vị khác 0 và có các số hàng trăm ví dụ 101 => thì biến daydu = true => và sẽ đọc một trăm lẻ một
            chuoi = " lẻ";
        }

        if (donvi == 5 && chuc >= 1)
            //Nếu đơn vị là số 5 và có hàng chục thì chuỗi sẽ là " lăm" chứ không phải là " năm"
            chuoi                                              += " lăm";
        else if (donvi > 1 || (donvi == 1 && chuc == 0)) chuoi += " " + mNumText[donvi];
        return chuoi;
    }

    private static string DocHangTram(double so, bool daydu)
    {
        string chuoi;
        //Lấy số hàng trăm ví du 434 / 100 = 4 (hàm Floor sẽ làm tròn số nguyên bé nhất)
        var tram = Convert.ToInt64(Math.Floor(so / 100));
        //Lấy phần còn lại của hàng trăm 434 % 100 = 34 (dư 34)
        so = so % 100;
        if (daydu || tram > 0)
        {
            chuoi =  " " + mNumText[tram] + " trăm";
            chuoi += DocHangChuc(so, true);
        }
        else
        {
            chuoi = DocHangChuc(so, false);
        }

        return chuoi;
    }

    private static string DocHangTrieu(double so, bool daydu)
    {
        var chuoi = "";
        //Lấy số hàng triệu
        var trieu = Convert.ToInt64(Math.Floor(so / 1000000));
        //Lấy phần dư sau số hàng triệu ví dụ 2,123,000 => so = 123,000
        so = so % 1000000;
        if (trieu > 0)
        {
            chuoi = DocHangTram(trieu, daydu) + " triệu";
            daydu = true;
        }

        //Lấy số hàng nghìn
        var nghin = Convert.ToInt64(Math.Floor(so / 1000));
        //Lấy phần dư sau số hàng nghin 
        so = so % 1000;
        if (nghin > 0)
        {
            chuoi += DocHangTram(nghin, daydu) + " nghìn";
            daydu =  true;
        }

        if (so > 0) chuoi += DocHangTram(so, daydu);
        return chuoi;
    }

#endregion
}