using System.Collections.Generic;
using Alpha.Services.Constants;

namespace Alpha.Services.Models
{
    public class Message
    {
        public Message()
        {
        }

        public Message(
            int id,
            int targetId,
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentKO,
            string contentVI,
            string betTransId)
        {
            Id = id;
            TargetId = targetId;
            ContentEN = contentEN;
            ContentCS = contentCS;
            ContentCH = contentCH;
            ContentJP = contentJP;
            ContentTH = contentTH;
            ContentKO = contentKO;
            ContentVI = contentVI;
            BetTransId = betTransId;
        }

        public int Id { get; }

        public int TargetId { get; }

        public string ContentEN { get; }

        public string ContentCS { get; }

        public string ContentCH { get; }

        public string ContentJP { get; }

        public string ContentTH { get; }

        public string ContentKO { get; }

        public string ContentVI { get; }

        public string BetTransId { get; }

        public IDictionary<string, string> Contents
            => new Dictionary<string, string>
            {
                [LanguageConstants.English] = ContentEN ?? string.Empty,
                [LanguageConstants.ChineseSimplified] = ContentCS ?? string.Empty,
                [LanguageConstants.ChineseTraditional] = ContentCH ?? string.Empty,
                [LanguageConstants.Japanese] = ContentJP ?? string.Empty,
                [LanguageConstants.Thai] = ContentTH ?? string.Empty,
                [LanguageConstants.Korean] = ContentKO ?? string.Empty,
                [LanguageConstants.Vietnamese] = ContentVI ?? string.Empty
            };

        public string GetContentByLanguage(string languageKey)
            => Contents.ContainsKey(languageKey) && !string.IsNullOrEmpty(Contents[languageKey])
                ? Contents[languageKey]
                : Contents[LanguageConstants.English];
    }
}