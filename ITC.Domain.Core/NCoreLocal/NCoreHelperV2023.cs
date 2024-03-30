using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ITC.Domain.Core.NCoreLocal;

public class NCoreHelperV2023
{
    public Guid ViewAuthor = Guid.Parse("815283D7-500A-4A35-B898-DB055A4DBE1C");

    public string ScheduleConfigDomain =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ScheduleConfig/domain.txt");

    public string ScheduleConfigPostFace =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ScheduleConfig/postFace.txt");

    public string HostWebsite =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ScheduleConfig/hostWebsite.txt");

    public string ReturnAvatarLink(bool isLocal, string filePath, string linkUrl)
    {
        if (isLocal) return filePath;

        return linkUrl;
    }

    public int ReturnScheduleConfigDomain()
    {
        var result = 0;
        var a      = File.ReadAllLines(ScheduleConfigDomain);
        foreach (var t in a)
        {
            result = Convert.ToInt32(t);
        }

        return result;
    }

    public int ReturnScheduleConfigPostFace()
    {
        var result = 0;
        var a      = File.ReadAllLines(ScheduleConfigPostFace);
        foreach (var t in a)
        {
            result = Convert.ToInt32(t);
        }

        return result;
    }

    /// <summary>
    /// Trả về host api
    /// </summary>
    /// <returns></returns>
    public string ReturnHostWebsite()
    {
        var result = "";
        var a      = File.ReadAllLines(HostWebsite);
        foreach (var t in a)
        {
            result = t.Trim();
        }

        return result;
    }

    public static string RewriteUrl(string unicode)
    {
        string pattern     = @"[^a-zA-Z]+";
        string replacement = " ";

        unicode = Regex.Replace(unicode, pattern, replacement);
        unicode = Regex.Replace(unicode, "[áàảãạăắằẳẵặâấầẩẫậ]", "a");
        unicode = Regex.Replace(unicode, "[óòỏõọôồốổỗộơớờởỡợ]", "o");
        unicode = Regex.Replace(unicode, "[éèẻẽẹêếềểễệ]", "e");
        unicode = Regex.Replace(unicode, "[íìỉĩị]", "i");
        unicode = Regex.Replace(unicode, "[úùủũụưứừửữự]", "u");
        unicode = Regex.Replace(unicode, "[ýỳỷỹỵ]", "y");
        unicode = Regex.Replace(unicode, "[đ]", "d");
        unicode = Regex.Replace(unicode, "\\W+", "-");
        unicode = unicode.Substring(0, Math.Min(unicode.Length, 30));
        return unicode.Trim('-');
    }
}