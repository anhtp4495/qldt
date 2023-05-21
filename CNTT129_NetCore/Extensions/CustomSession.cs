using Microsoft.AspNetCore.Mvc;

namespace CNTT129_NetCore.Extensions
{
    public class CustomSession
    {
        private ISession _session;
        public CustomSession(ISession session)
        {
            this._session = session;
        }

        public ISession Session
        {
            get => this._session;
        }

        public object? this[string key]
        {
            get => this._session.Get<object>(key);
            set => this._session.Set<object>(key, value);
        }
    }
}
