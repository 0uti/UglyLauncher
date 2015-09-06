using System;

namespace UglyLauncher.Minecraft
{
    [Serializable]
    class MCInvalidCredentialsException : Exception
    {
        public MCInvalidCredentialsException() : base("mööp.") { }
        public MCInvalidCredentialsException(Exception e) : base("mööp.", e) { }
        public MCInvalidCredentialsException(String message, Exception e) : base(message, e) { }
        public MCInvalidCredentialsException(String message) : base(message) { }
    }

    [Serializable]
    class MCUserMigratedException : Exception
    {
        public MCUserMigratedException() : base("mööp.") { }
        public MCUserMigratedException(Exception e) : base("mööp.", e) { }
        public MCUserMigratedException(String message, Exception e) : base(message, e) { }
        public MCUserMigratedException(String message) : base(message) { }
    }

    [Serializable]
    class MCInvalidTokenException : Exception
    {
        public MCInvalidTokenException() : base("mööp.") { }
        public MCInvalidTokenException(Exception e) : base("mööp.", e) { }
        public MCInvalidTokenException(String message, Exception e) : base(message, e) { }
        public MCInvalidTokenException(String message) : base(message) { }
    }
}
