using System;
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
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentKO,
            string contentVI,
            DateTime postTime,
            DateTime createTime,
            DateTime lastUpdateTime,
            byte messageType,
            bool isSticky,
            bool isTest,
            int matchId,
            int creator,
            int lastUpdator,
            string creatorName,
            string lastUpdateName,
            bool isBatch,
            string bettransId,
            int userId,
            string userName,
            long rowNum)
        {
            Id = id;
            ContentEN = contentEN;
            ContentCS = contentCS;
            ContentCH = contentCH;
            ContentJP = contentJP;
            ContentTH = contentTH;
            ContentKO = contentKO;
            ContentVI = contentVI;

            PostTime = postTime;
            CreateTime = createTime;
            LastUpdateTime = lastUpdateTime;
            MessageType = messageType;
            IsSticky = isSticky;
            IsTest = isTest;
            MatchId = matchId;
            CreatorId = creator;
            LastUpdator = lastUpdator;
            CreatorName = creatorName;
            LastUpdateName = lastUpdateName;
            IsBatch = isBatch;
            BetTransId = bettransId;
            UserId = userId;
            UserName = userName;
            RowNum = rowNum;
        }

        public int Id { get; }

        public string ContentEN { get; }

        public string ContentCS { get; }

        public string ContentCH { get; }

        public string ContentJP { get; }

        public string ContentTH { get; }

        public string ContentKO { get; }

        public string ContentVI { get; }

        public DateTime PostTime { get; }

        public DateTime CreateTime { get; }

        public DateTime LastUpdateTime { get; }

        public byte MessageType { get; }

        public bool IsSticky { get; }

        public bool IsTest { get; }

        public int MatchId { get; }

        public int CreatorId { get; }

        public int LastUpdator { get; }

        public string CreatorName { get; }

        public string LastUpdateName { get; }

        public bool IsBatch { get; }

        public string BetTransId { get; }

        public int UserId { get; }

        public string UserName { get; }

        public long RowNum { get; }

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