using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterApi.Core.Stores
{
    public interface IScheduleStore:IBaseStore<Schedule>
    {
        public Task<bool> Update(string id, int semesterNumber, IReadOnlyCollection<ScheduleGroup> groupsSchedule);
        public Task<bool> AddGroup(string id, ScheduleGroup group);
        public Task<bool> RemoveGroup(string id,string groupId);
        public Task<bool> AddWeeksOfGroup(string id,string groupId,ScheduledClassOfWeeks scheduleClassOfWeeks);
        public Task<bool> RemoveWeeksOfGroup(string id, string groupId,string weekId);
        public Task<bool> AddClassOfWeek(string id, string groupId, string weekId, ScheduledClass scheduledClass);
        public Task<bool> RemoveClassOfWeek(string id,string groupId,string weekId,string classId);
    }
}
