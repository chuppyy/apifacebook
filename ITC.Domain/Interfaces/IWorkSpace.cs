namespace ITC.Domain.Interfaces;

public interface IWorkSpace
{
#region Properties

    string Id_DonVi             { get; set; }
    string Id_DonViDanhGia      { get; set; }
    string Id_NienKhoa          { get; set; }
    string Id_NienKhoaDvDanhGia { get; set; }
    string Name                 { get; set; }
    string RoleIdentity         { get; set; }
    string UserId               { get; set; }

#endregion

    //List<CustomModuleModel> CustomModuleModels { get; set; }

    //void SetSession(IWorkSpace value);
    //IWorkSpace GetSession();
}