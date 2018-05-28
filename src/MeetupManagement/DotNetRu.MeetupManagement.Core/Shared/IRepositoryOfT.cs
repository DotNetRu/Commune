using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Core.Shared
{
    public interface IRepository<TEntity>
    {
        TEntity Get(long id);
        long Create(TEntity draftTalk);
        void Update(TEntity draftTalk);
        void Delete(long id);
    }
}
