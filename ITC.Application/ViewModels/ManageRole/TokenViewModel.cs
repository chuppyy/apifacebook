namespace ITC.Application.ViewModels.ManageRole;

public class TokenViewModel
{
#region Constructors

    public TokenViewModel()
    {
    }

    public TokenViewModel(string accessToken, string refreshToken)
    {
        AccessToken  = accessToken;
        RefreshToken = refreshToken;
    }

#endregion

#region Properties

    public string AccessToken  { get; set; }
    public string RefreshToken { get; set; }

#endregion
}

public class LogoutViewModel
{
    /// <summary>
    ///     Mã định danh
    /// </summary>
    public string Identifiers { get; set; }
}