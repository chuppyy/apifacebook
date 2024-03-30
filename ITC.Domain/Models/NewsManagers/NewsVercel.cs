namespace ITC.Domain.Models.NewsManagers;

public class NewsVercel
{
    #region Properties

    /// <summary>
    ///     Mã chức năng
    /// </summary>
    public int Id { get; set; }

    public string Name      { get; set; }
    public bool   IsDeleted { get; set; }

    #endregion
}