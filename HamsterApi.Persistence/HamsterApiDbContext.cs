 

using System;
using System.Collections.Generic;
using System.Linq;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;

using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence 
{
    internal partial class HamsterApiDbContext : BrightstarEntityContext {
    	
    	static HamsterApiDbContext() 
    	{
            InitializeEntityMappingStore();
        }
        public static void InitializeEntityMappingStore()
        {
    		var provider = new ReflectionMappingProvider();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity, HamsterApi.Persistence.Entites.Interfaces.AcademicLoadEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IAuditoriumEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IAuditoriumEntity, HamsterApi.Persistence.Entites.Interfaces.AuditoriumEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IChairEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IChairEntity, HamsterApi.Persistence.Entites.Interfaces.ChairEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.ICurriculumEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.ICurriculumEntity, HamsterApi.Persistence.Entites.Interfaces.CurriculumEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IDepartmentEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IDepartmentEntity, HamsterApi.Persistence.Entites.Interfaces.DepartmentEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IDirectionEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IDirectionEntity, HamsterApi.Persistence.Entites.Interfaces.DirectionEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IGroupEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IGroupEntity, HamsterApi.Persistence.Entites.Interfaces.GroupEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity, HamsterApi.Persistence.Entites.Interfaces.ScheduledClassEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity, HamsterApi.Persistence.Entites.Interfaces.ScheduledClassOfWeeksEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity, HamsterApi.Persistence.Entites.Interfaces.ScheduledWeekEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduleEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IScheduleEntity, HamsterApi.Persistence.Entites.Interfaces.ScheduleEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduleGroupEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.IScheduleGroupEntity, HamsterApi.Persistence.Entites.Interfaces.ScheduleGroupEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.ISemesterEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.ISemesterEntity, HamsterApi.Persistence.Entites.Interfaces.SemesterEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity, HamsterApi.Persistence.Entites.Interfaces.SubjectEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity, HamsterApi.Persistence.Entites.Interfaces.SubjectWtihLoadEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.ITeacherEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.ITeacherEntity, HamsterApi.Persistence.Entites.Interfaces.TeacherEntity>();
    		provider.AddMappingsForType(EntityMappingStore.Instance, typeof(HamsterApi.Persistence.Entites.Interfaces.ITeachingLoadEntity));
    		EntityMappingStore.Instance.SetImplMapping<HamsterApi.Persistence.Entites.Interfaces.ITeachingLoadEntity, HamsterApi.Persistence.Entites.Interfaces.TeachingLoadEntity>();
    	}
    	
    	public HamsterApiDbContext(IDataObjectStore dataObjectStore) : base(dataObjectStore)
    	{
    		InitializeContext();
    	}
    
    	public HamsterApiDbContext(
    	    string connectionString, 
    		bool? enableOptimisticLocking=null,
    		string updateGraphUri = null,
    		IEnumerable<string> datasetGraphUris = null,
    		string versionGraphUri = null
        ) : base(connectionString, enableOptimisticLocking, updateGraphUri, datasetGraphUris, versionGraphUri)
    	{
    		InitializeContext();
    	}
    
    	public HamsterApiDbContext() : base()
    	{
    		InitializeContext();
    	}
    
    	public HamsterApiDbContext(
    		string updateGraphUri,
    		IEnumerable<string> datasetGraphUris,
    		string versionGraphUri
    	) : base(updateGraphUri:updateGraphUri, datasetGraphUris:datasetGraphUris, versionGraphUri:versionGraphUri)
    	{
    		InitializeContext();
    	}
    	
    	private void InitializeContext() 
    	{
    		AcademicLoadEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity>(this);
    		AuditoriumEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IAuditoriumEntity>(this);
    		ChairEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IChairEntity>(this);
    		CurriculumEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.ICurriculumEntity>(this);
    		DepartmentEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IDepartmentEntity>(this);
    		DirectionEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IDirectionEntity>(this);
    		GroupEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IGroupEntity>(this);
    		ScheduledClassEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity>(this);
    		ScheduledClassOfWeeksEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity>(this);
    		ScheduledWeekEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity>(this);
    		ScheduleEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduleEntity>(this);
    		ScheduleGroupEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduleGroupEntity>(this);
    		SemesterEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.ISemesterEntity>(this);
    		SubjectEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity>(this);
    		SubjectWtihLoadEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity>(this);
    		TeacherEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.ITeacherEntity>(this);
    		TeachingLoadEntities = 	new BrightstarEntitySet<HamsterApi.Persistence.Entites.Interfaces.ITeachingLoadEntity>(this);
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity> AcademicLoadEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IAuditoriumEntity> AuditoriumEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IChairEntity> ChairEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.ICurriculumEntity> CurriculumEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IDepartmentEntity> DepartmentEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IDirectionEntity> DirectionEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IGroupEntity> GroupEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity> ScheduledClassEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity> ScheduledClassOfWeeksEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity> ScheduledWeekEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduleEntity> ScheduleEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.IScheduleGroupEntity> ScheduleGroupEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.ISemesterEntity> SemesterEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity> SubjectEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity> SubjectWtihLoadEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.ITeacherEntity> TeacherEntities
    	{
    		get; private set;
    	}
    	
    	internal IEntitySet<HamsterApi.Persistence.Entites.Interfaces.ITeachingLoadEntity> TeachingLoadEntities
    	{
    		get; private set;
    	}
    	
        public IEntitySet<T> EntitySet<T>() where T : class {
            var itemType = typeof(T);
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity))) {
                return (IEntitySet<T>)this.AcademicLoadEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IAuditoriumEntity))) {
                return (IEntitySet<T>)this.AuditoriumEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IChairEntity))) {
                return (IEntitySet<T>)this.ChairEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.ICurriculumEntity))) {
                return (IEntitySet<T>)this.CurriculumEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IDepartmentEntity))) {
                return (IEntitySet<T>)this.DepartmentEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IDirectionEntity))) {
                return (IEntitySet<T>)this.DirectionEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IGroupEntity))) {
                return (IEntitySet<T>)this.GroupEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity))) {
                return (IEntitySet<T>)this.ScheduledClassEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity))) {
                return (IEntitySet<T>)this.ScheduledClassOfWeeksEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity))) {
                return (IEntitySet<T>)this.ScheduledWeekEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduleEntity))) {
                return (IEntitySet<T>)this.ScheduleEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.IScheduleGroupEntity))) {
                return (IEntitySet<T>)this.ScheduleGroupEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.ISemesterEntity))) {
                return (IEntitySet<T>)this.SemesterEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity))) {
                return (IEntitySet<T>)this.SubjectEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity))) {
                return (IEntitySet<T>)this.SubjectWtihLoadEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.ITeacherEntity))) {
                return (IEntitySet<T>)this.TeacherEntities;
            }
            if (typeof(T).Equals(typeof(HamsterApi.Persistence.Entites.Interfaces.ITeachingLoadEntity))) {
                return (IEntitySet<T>)this.TeachingLoadEntities;
            }
            throw new InvalidOperationException(typeof(T).FullName + " is not a recognized entity interface type.");
        }
    
        } // end class HamsterApiDbContext
        
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class AcademicLoadEntity : BrightstarEntityObject, IAcademicLoadEntity 
    {
    	public AcademicLoadEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public AcademicLoadEntity(BrightstarEntityContext context) : base(context, typeof(AcademicLoadEntity)) { }
    	public AcademicLoadEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity
    
    	public System.Int32 Lectures
    	{
            		get { return GetRelatedProperty<System.Int32>("Lectures"); }
            		set { SetRelatedProperty("Lectures", value); }
    	}
    
    	public System.Int32 Laboratory
    	{
            		get { return GetRelatedProperty<System.Int32>("Laboratory"); }
            		set { SetRelatedProperty("Laboratory", value); }
    	}
    
    	public System.Int32 Practice
    	{
            		get { return GetRelatedProperty<System.Int32>("Practice"); }
            		set { SetRelatedProperty("Practice", value); }
    	}
    
    	public System.Int32 Credits
    	{
            		get { return GetRelatedProperty<System.Int32>("Credits"); }
            		set { SetRelatedProperty("Credits", value); }
    	}
    
    	public System.Int32 Total
    	{
            		get { return GetRelatedProperty<System.Int32>("Total"); }
            		set { SetRelatedProperty("Total", value); }
    	}
    
    	public HamsterApi.Domain.Common.Enum.AcademicEvaluationType AcademicEvaluationType
    	{
            		get { return GetRelatedProperty<HamsterApi.Domain.Common.Enum.AcademicEvaluationType>("AcademicEvaluationType"); }
            		set { SetRelatedProperty("AcademicEvaluationType", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class AuditoriumEntity : BrightstarEntityObject, IAuditoriumEntity 
    {
    	public AuditoriumEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public AuditoriumEntity(BrightstarEntityContext context) : base(context, typeof(AuditoriumEntity)) { }
    	public AuditoriumEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IAuditoriumEntity
    
    	public System.String Number
    	{
            		get { return GetRelatedProperty<System.String>("Number"); }
            		set { SetRelatedProperty("Number", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class ChairEntity : BrightstarEntityObject, IChairEntity 
    {
    	public ChairEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public ChairEntity(BrightstarEntityContext context) : base(context, typeof(ChairEntity)) { }
    	public ChairEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IChairEntity
    
    	public System.String Title
    	{
            		get { return GetRelatedProperty<System.String>("Title"); }
            		set { SetRelatedProperty("Title", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> TeachersIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("TeachersIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("TeachersIds", value); }
    	}
    
    	public System.String DepartmentId
    	{
            		get { return GetRelatedProperty<System.String>("DepartmentId"); }
            		set { SetRelatedProperty("DepartmentId", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class CurriculumEntity : BrightstarEntityObject, ICurriculumEntity 
    {
    	public CurriculumEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public CurriculumEntity(BrightstarEntityContext context) : base(context, typeof(CurriculumEntity)) { }
    	public CurriculumEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.ICurriculumEntity
    
    	public System.String ChairId
    	{
            		get { return GetRelatedProperty<System.String>("ChairId"); }
            		set { SetRelatedProperty("ChairId", value); }
    	}
    
    	public System.String DepartmentId
    	{
            		get { return GetRelatedProperty<System.String>("DepartmentId"); }
            		set { SetRelatedProperty("DepartmentId", value); }
    	}
    
    	public System.String DirectionId
    	{
            		get { return GetRelatedProperty<System.String>("DirectionId"); }
            		set { SetRelatedProperty("DirectionId", value); }
    	}
    
    	public System.Int32 YearOfPreparation
    	{
            		get { return GetRelatedProperty<System.Int32>("YearOfPreparation"); }
            		set { SetRelatedProperty("YearOfPreparation", value); }
    	}
    
    	public System.String FGOSNumber
    	{
            		get { return GetRelatedProperty<System.String>("FGOSNumber"); }
            		set { SetRelatedProperty("FGOSNumber", value); }
    	}
    	public System.Collections.Generic.ICollection<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity> SemestersSubjects
    	{
    		get { return GetRelatedObjects<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity>("SemestersSubjects"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("SemestersSubjects", value); }
    								}
    	public System.Collections.Generic.ICollection<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity> SemestersElectiveSubjects
    	{
    		get { return GetRelatedObjects<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity>("SemestersElectiveSubjects"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("SemestersElectiveSubjects", value); }
    								}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class DepartmentEntity : BrightstarEntityObject, IDepartmentEntity 
    {
    	public DepartmentEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public DepartmentEntity(BrightstarEntityContext context) : base(context, typeof(DepartmentEntity)) { }
    	public DepartmentEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IDepartmentEntity
    
    	public System.String Title
    	{
            		get { return GetRelatedProperty<System.String>("Title"); }
            		set { SetRelatedProperty("Title", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> ChairsIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("ChairsIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("ChairsIds", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> DirectionsIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("DirectionsIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("DirectionsIds", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class DirectionEntity : BrightstarEntityObject, IDirectionEntity 
    {
    	public DirectionEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public DirectionEntity(BrightstarEntityContext context) : base(context, typeof(DirectionEntity)) { }
    	public DirectionEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IDirectionEntity
    
    	public System.String Title
    	{
            		get { return GetRelatedProperty<System.String>("Title"); }
            		set { SetRelatedProperty("Title", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> GroupsIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("GroupsIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("GroupsIds", value); }
    	}
    
    	public HamsterApi.Domain.Common.Enum.LevelOfEducation LevelOfEducation
    	{
            		get { return GetRelatedProperty<HamsterApi.Domain.Common.Enum.LevelOfEducation>("LevelOfEducation"); }
            		set { SetRelatedProperty("LevelOfEducation", value); }
    	}
    
    	public HamsterApi.Domain.Common.Enum.FormOfEducation FormOfEducation
    	{
            		get { return GetRelatedProperty<HamsterApi.Domain.Common.Enum.FormOfEducation>("FormOfEducation"); }
            		set { SetRelatedProperty("FormOfEducation", value); }
    	}
    
    	public System.String DepartmentId
    	{
            		get { return GetRelatedProperty<System.String>("DepartmentId"); }
            		set { SetRelatedProperty("DepartmentId", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class GroupEntity : BrightstarEntityObject, IGroupEntity 
    {
    	public GroupEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public GroupEntity(BrightstarEntityContext context) : base(context, typeof(GroupEntity)) { }
    	public GroupEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IGroupEntity
    
    	public System.String Number
    	{
            		get { return GetRelatedProperty<System.String>("Number"); }
            		set { SetRelatedProperty("Number", value); }
    	}
    
    	public System.String DirectionId
    	{
            		get { return GetRelatedProperty<System.String>("DirectionId"); }
            		set { SetRelatedProperty("DirectionId", value); }
    	}
    
    	public HamsterApi.Domain.Common.Enum.LevelOfEducation LevelOfEducation
    	{
            		get { return GetRelatedProperty<HamsterApi.Domain.Common.Enum.LevelOfEducation>("LevelOfEducation"); }
            		set { SetRelatedProperty("LevelOfEducation", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class ScheduledClassEntity : BrightstarEntityObject, IScheduledClassEntity 
    {
    	public ScheduledClassEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public ScheduledClassEntity(BrightstarEntityContext context) : base(context, typeof(ScheduledClassEntity)) { }
    	public ScheduledClassEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity
    
    	public HamsterApi.Domain.Common.Enum.ClassType ClassType
    	{
            		get { return GetRelatedProperty<HamsterApi.Domain.Common.Enum.ClassType>("ClassType"); }
            		set { SetRelatedProperty("ClassType", value); }
    	}
    
    	public System.Int32 ClassNumber
    	{
            		get { return GetRelatedProperty<System.Int32>("ClassNumber"); }
            		set { SetRelatedProperty("ClassNumber", value); }
    	}
    
    	public System.String SubjectId
    	{
            		get { return GetRelatedProperty<System.String>("SubjectId"); }
            		set { SetRelatedProperty("SubjectId", value); }
    	}
    
    	public System.String TeacherId
    	{
            		get { return GetRelatedProperty<System.String>("TeacherId"); }
            		set { SetRelatedProperty("TeacherId", value); }
    	}
    
    	public System.String AuditoriumId
    	{
            		get { return GetRelatedProperty<System.String>("AuditoriumId"); }
            		set { SetRelatedProperty("AuditoriumId", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class ScheduledClassOfWeeksEntity : BrightstarEntityObject, IScheduledClassOfWeeksEntity 
    {
    	public ScheduledClassOfWeeksEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public ScheduledClassOfWeeksEntity(BrightstarEntityContext context) : base(context, typeof(ScheduledClassOfWeeksEntity)) { }
    	public ScheduledClassOfWeeksEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity
    
    	public System.DayOfWeek DayOfWeek
    	{
            		get { return GetRelatedProperty<System.DayOfWeek>("DayOfWeek"); }
            		set { SetRelatedProperty("DayOfWeek", value); }
    	}
    
    	public System.String Date
    	{
            		get { return GetRelatedProperty<System.String>("Date"); }
            		set { SetRelatedProperty("Date", value); }
    	}
    	public System.Collections.Generic.ICollection<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity> ScheduledClasses
    	{
    		get { return GetRelatedObjects<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassEntity>("ScheduledClasses"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("ScheduledClasses", value); }
    								}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class ScheduledWeekEntity : BrightstarEntityObject, IScheduledWeekEntity 
    {
    	public ScheduledWeekEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public ScheduledWeekEntity(BrightstarEntityContext context) : base(context, typeof(ScheduledWeekEntity)) { }
    	public ScheduledWeekEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity
    
    	public System.Int32 WeekNumber
    	{
            		get { return GetRelatedProperty<System.Int32>("WeekNumber"); }
            		set { SetRelatedProperty("WeekNumber", value); }
    	}
    	public System.Collections.Generic.ICollection<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity> ClassOfWeeks
    	{
    		get { return GetRelatedObjects<HamsterApi.Persistence.Entites.Interfaces.IScheduledClassOfWeeksEntity>("ClassOfWeeks"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("ClassOfWeeks", value); }
    								}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class ScheduleEntity : BrightstarEntityObject, IScheduleEntity 
    {
    	public ScheduleEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public ScheduleEntity(BrightstarEntityContext context) : base(context, typeof(ScheduleEntity)) { }
    	public ScheduleEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IScheduleEntity
    
    	public System.Int32 Year
    	{
            		get { return GetRelatedProperty<System.Int32>("Year"); }
            		set { SetRelatedProperty("Year", value); }
    	}
    
    	public HamsterApi.Domain.Common.Enum.SpringOrAutumn SpringOrAutumn
    	{
            		get { return GetRelatedProperty<HamsterApi.Domain.Common.Enum.SpringOrAutumn>("SpringOrAutumn"); }
            		set { SetRelatedProperty("SpringOrAutumn", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> GroupsScheduleIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("GroupsScheduleIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("GroupsScheduleIds", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class ScheduleGroupEntity : BrightstarEntityObject, IScheduleGroupEntity 
    {
    	public ScheduleGroupEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public ScheduleGroupEntity(BrightstarEntityContext context) : base(context, typeof(ScheduleGroupEntity)) { }
    	public ScheduleGroupEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.IScheduleGroupEntity
    
    	public System.String ScheduleId
    	{
            		get { return GetRelatedProperty<System.String>("ScheduleId"); }
            		set { SetRelatedProperty("ScheduleId", value); }
    	}
    
    	public System.String GroupId
    	{
            		get { return GetRelatedProperty<System.String>("GroupId"); }
            		set { SetRelatedProperty("GroupId", value); }
    	}
    
    	public System.Int32 SemesterNumber
    	{
            		get { return GetRelatedProperty<System.Int32>("SemesterNumber"); }
            		set { SetRelatedProperty("SemesterNumber", value); }
    	}
    	public System.Collections.Generic.ICollection<HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity> Weeks
    	{
    		get { return GetRelatedObjects<HamsterApi.Persistence.Entites.Interfaces.IScheduledWeekEntity>("Weeks"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("Weeks", value); }
    								}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class SemesterEntity : BrightstarEntityObject, ISemesterEntity 
    {
    	public SemesterEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public SemesterEntity(BrightstarEntityContext context) : base(context, typeof(SemesterEntity)) { }
    	public SemesterEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.ISemesterEntity
    
    	public System.Int32 Number
    	{
            		get { return GetRelatedProperty<System.Int32>("Number"); }
            		set { SetRelatedProperty("Number", value); }
    	}
    
    	public System.String GroupId
    	{
            		get { return GetRelatedProperty<System.String>("GroupId"); }
            		set { SetRelatedProperty("GroupId", value); }
    	}
    	public System.Collections.Generic.ICollection<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity> Subjects
    	{
    		get { return GetRelatedObjects<HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity>("Subjects"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("Subjects", value); }
    								}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class SubjectEntity : BrightstarEntityObject, ISubjectEntity 
    {
    	public SubjectEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public SubjectEntity(BrightstarEntityContext context) : base(context, typeof(SubjectEntity)) { }
    	public SubjectEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity
    
    	public System.String Title
    	{
            		get { return GetRelatedProperty<System.String>("Title"); }
            		set { SetRelatedProperty("Title", value); }
    	}
    
    	public System.String Index
    	{
            		get { return GetRelatedProperty<System.String>("Index"); }
            		set { SetRelatedProperty("Index", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> TeachersIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("TeachersIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("TeachersIds", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class SubjectWtihLoadEntity : BrightstarEntityObject, ISubjectWtihLoadEntity 
    {
    	public SubjectWtihLoadEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public SubjectWtihLoadEntity(BrightstarEntityContext context) : base(context, typeof(SubjectWtihLoadEntity)) { }
    	public SubjectWtihLoadEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.ISubjectWtihLoadEntity
    
    	public System.Int32 SemesterNumber
    	{
            		get { return GetRelatedProperty<System.Int32>("SemesterNumber"); }
            		set { SetRelatedProperty("SemesterNumber", value); }
    	}
    
    	public HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity Subject
    	{
            get { return GetRelatedObject<HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity>("Subject"); }
            set { SetRelatedObject<HamsterApi.Persistence.Entites.Interfaces.ISubjectEntity>("Subject", value); }
    	}
    
    	public HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity AcademicLoad
    	{
            get { return GetRelatedObject<HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity>("AcademicLoad"); }
            set { SetRelatedObject<HamsterApi.Persistence.Entites.Interfaces.IAcademicLoadEntity>("AcademicLoad", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class TeacherEntity : BrightstarEntityObject, ITeacherEntity 
    {
    	public TeacherEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public TeacherEntity(BrightstarEntityContext context) : base(context, typeof(TeacherEntity)) { }
    	public TeacherEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.ITeacherEntity
    
    	public System.String Name
    	{
            		get { return GetRelatedProperty<System.String>("Name"); }
            		set { SetRelatedProperty("Name", value); }
    	}
    
    	public System.String Surname
    	{
            		get { return GetRelatedProperty<System.String>("Surname"); }
            		set { SetRelatedProperty("Surname", value); }
    	}
    
    	public System.String Patronymic
    	{
            		get { return GetRelatedProperty<System.String>("Patronymic"); }
            		set { SetRelatedProperty("Patronymic", value); }
    	}
    
    	public System.String FullName
    	{
            		get { return GetRelatedProperty<System.String>("FullName"); }
            		set { SetRelatedProperty("FullName", value); }
    	}
    	public System.Collections.Generic.ICollection<System.String> SubjectsIds
    	{
    		get { return GetRelatedLiteralPropertiesCollection<System.String>("SubjectsIds"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedLiteralPropertiesCollection<System.String>("SubjectsIds", value); }
    	}
    
    	public System.String ChairId
    	{
            		get { return GetRelatedProperty<System.String>("ChairId"); }
            		set { SetRelatedProperty("ChairId", value); }
    	}
    
    	public System.String TeacherLoadId
    	{
            		get { return GetRelatedProperty<System.String>("TeacherLoadId"); }
            		set { SetRelatedProperty("TeacherLoadId", value); }
    	}
    	#endregion
    }
}
namespace HamsterApi.Persistence.Entites.Interfaces 
{
    
    internal partial class TeachingLoadEntity : BrightstarEntityObject, ITeachingLoadEntity 
    {
    	public TeachingLoadEntity(BrightstarEntityContext context, BrightstarDB.Client.IDataObject dataObject) : base(context, dataObject) { }
        public TeachingLoadEntity(BrightstarEntityContext context) : base(context, typeof(TeachingLoadEntity)) { }
    	public TeachingLoadEntity() : base() { }
    	public System.String Id { get {return GetKey(); } set { SetKey(value); } }
    	#region Implementation of HamsterApi.Persistence.Entites.Interfaces.ITeachingLoadEntity
    
    	public System.Int32 LecturesHours
    	{
            		get { return GetRelatedProperty<System.Int32>("LecturesHours"); }
            		set { SetRelatedProperty("LecturesHours", value); }
    	}
    
    	public System.Int32 PracticeHours
    	{
            		get { return GetRelatedProperty<System.Int32>("PracticeHours"); }
            		set { SetRelatedProperty("PracticeHours", value); }
    	}
    
    	public System.Int32 LaboratoryHours
    	{
            		get { return GetRelatedProperty<System.Int32>("LaboratoryHours"); }
            		set { SetRelatedProperty("LaboratoryHours", value); }
    	}
    
    	public System.Int32 LecturesHoursMax
    	{
            		get { return GetRelatedProperty<System.Int32>("LecturesHoursMax"); }
            		set { SetRelatedProperty("LecturesHoursMax", value); }
    	}
    
    	public System.Int32 PracticeHoursMax
    	{
            		get { return GetRelatedProperty<System.Int32>("PracticeHoursMax"); }
            		set { SetRelatedProperty("PracticeHoursMax", value); }
    	}
    
    	public System.Int32 LaboratoryHoursMax
    	{
            		get { return GetRelatedProperty<System.Int32>("LaboratoryHoursMax"); }
            		set { SetRelatedProperty("LaboratoryHoursMax", value); }
    	}
    	#endregion
    }
}
