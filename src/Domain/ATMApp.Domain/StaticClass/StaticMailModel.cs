using ATMApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.StaticClass
{
    public static class StaticMailModel
    {
        public static MailModel successMail = new MailModel()
        {
            Body = "İşlem Başarılı bir şekilde gerçekleşmiştir.",
            Subject = "Başarılı işlem"
        };
        public static MailModel failedMail = new MailModel()
        {
            Body = "İşlem Başarısız bir şekilde gerçekleşmiştir.",
            Subject = "Başarısız İşlem",
        };
    }
}
