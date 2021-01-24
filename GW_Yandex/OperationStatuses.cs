using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Yandex
{
    public class OperationStatuses
    {
        private Dictionary<int, string> statuses;
        private static OperationStatuses instance;
        private OperationStatuses()
        {
            statuses = new Dictionary<int, string>();
            this[0] = "Успех. Обработка завершена. Запрос выполнен успешно.";
            this[1] = "В обработке. Запрос в процессе обработки. Возвращается, если истекло время ожидания завершения обработки запроса. Требуется повторить запрос для уточнения результата.";
            this[3] = "Отвергнут. Обработка завершена. Запрос отвергнут. Причина отказа передается в параметре error.";
        }
        public string this[int code]
        {
            get => statuses[code];
            private set
            {
                statuses.Add(code, value);
            }
        }

        public static OperationStatuses Instance 
        {
            get
            {
                if (instance == null)
                    instance = new OperationStatuses();
                return instance;
            }
        }
    }
}
