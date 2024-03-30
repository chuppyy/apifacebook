namespace ITC.Domain.Models.NewsManagers;

public class NewsConfig
{
    #region Properties

    /// <summary>
    ///     Mã chức năng
    /// </summary>
    public int Id { get; set; }

    public string TokenGit          { get; set; }
    public string OwnerGit          { get; set; }
    public string ProjectDefaultGit { get; set; }
    public string TeamId            { get; set; }
    public string TokenVercel       { get; set; }
    public string Host       { get; set; }

    #endregion
}