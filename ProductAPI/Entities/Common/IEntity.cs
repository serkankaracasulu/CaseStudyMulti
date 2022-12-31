﻿namespace ProductAPI.Entities.Common
{
    public interface IEntity<T> where T : struct
    {
        public T Id { get; set; }
    }
}
