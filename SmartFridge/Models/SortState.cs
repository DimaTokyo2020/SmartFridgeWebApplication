using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Models
{
    public enum SortState
    {
    NameAsc,    // по имени по возрастанию
    NameDesc,   // по имени по убыванию
    DeviceNAmeAsc, // по возрасту по возрастанию
    DeviceNAmeDesc,    // по возрасту по убыванию
    CountAsc, // по компании по возрастанию
    CountDesc // по компании по убыванию
    }
}
