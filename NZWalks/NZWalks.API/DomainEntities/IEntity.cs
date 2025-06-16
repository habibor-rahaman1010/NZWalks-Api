﻿namespace NZWalks.API.DomainEntities
{
    public interface IEntity<T> where T : IComparable<T>
    {
        public T Id { get; set; }
    }
}
