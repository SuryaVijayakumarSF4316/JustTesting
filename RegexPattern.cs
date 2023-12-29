using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment
{
    public class RegexPattern
    {
        public const string NotificationTypeRegex="NotificationType:(.*),ServerId";
        public const string ServerIdRegex=",ServerId:(.*),MessageStream";
        public const string MessageStreamRegex=",MessageStream:(.*),MessageId";
        public const string MessageIdRegex=",MessageId:(.*),Tag";
        public const string TagRegex=",Tag:(.*),Recipient:";
        public const string RecipientRegex=",Recipient:(.*),DeliveredAt";
        public const string DelieveryAtRegex=",DeliveredAt:(.*),Details";
        public const string DetailsRegex=",Details:(.*),ToList";
        public const string ToListRegex=",ToList:(.*),CC";
        public const string CCRegex=",CC:(.*),BCC";
        public const string BCCRegex=",BCC:(.*),Recipients";
        public const string RecipientsRegex=",Recipients:(.*),Subject";
        public const string SubjectRegex=",Subject:(.*),From";
        public const string FromRegex=",From:(.*),Status";
        public const string StatusRegex=",Status:(.*),MessageEvents";
        public const string MessageEventsRegex=",MessageEvents:(.*)";


    }
}