using System;

namespace Application.Repositories
{
    public interface IEditorsRepository
    {
        Guid Login(Editor editor);
        string[] GetRights(Editor editor);
    }
}