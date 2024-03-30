namespace ITC.Domain.Core.Events;

public interface IHandler<in T> where T : Message
{
#region Methods

    void Handle(T message);

#endregion
}