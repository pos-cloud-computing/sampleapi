namespace Domain.Common.Entity
{
	internal interface IEntity<TId>
	{
		public TId Id { get; }
		public Boolean Active { get; }
		protected void ValidateId(TId id);
	}
}

