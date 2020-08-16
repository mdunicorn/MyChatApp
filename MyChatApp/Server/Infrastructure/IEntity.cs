using System;

namespace MyChatApp.Server.Infrastructure
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}
