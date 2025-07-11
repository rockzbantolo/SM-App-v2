using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static ServiceDesk.Core.Shared.EnvMode;

namespace ServiceDesk.Core.Shared
{
    public static class Logger
    {
        private static EnvSetting? _envSettings;

        public static void Init(EnvSetting envSettings)
        {
            _envSettings = envSettings;
        }

        public static void Trace(string message,
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        {
            if (!IsProd(_envSettings?.Environment)) return;

            WriteLogs("TRACE", message, caller, file, line);
        }

        public static void Info(string message,
         [CallerMemberName] string caller = "",
         [CallerFilePath] string file = "",
         [CallerLineNumber] int line = 0)
        {
            if (!IsDev(_envSettings?.Environment)) return;

            // Wrap lines at 100 characters
            //const int wrapLength = 150;
            //var wrappedMessage = WrapMessage("", message, wrapLength);

            WriteLogs("INFO", message, caller, file, line);
        }

        public static void Warn(string message,
         [CallerMemberName] string caller = "",
         [CallerFilePath] string file = "",
         [CallerLineNumber] int line = 0)
        {
            if (!IsDev(_envSettings?.Environment)) return;

            WriteLogs("WARN", message, caller, file, line);
            
        }

        public static void Error(string message, Exception ex = null,
         [CallerMemberName] string caller = "",
         [CallerFilePath] string file = "",
         [CallerLineNumber] int line = 0)
        {
            if (!IsDev(_envSettings?.Environment)) return;

            var sb = new StringBuilder();

            sb.AppendLine($"{message}");

            if (ex != null)
            {
                sb.AppendLine($"-[Reason]   : {ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message}");
                sb.AppendLine($"-[Exception]: {ex.GetType().Name}");

                // Add all inner exceptions
                var inner = ex.InnerException;
                int depth = 1;
                while (inner != null)
                {
                    sb.AppendLine($" -[Inner#{depth}]    : {inner.GetType().Name}-{inner.Message}");
                    inner = inner.InnerException;
                    depth++;
                }

                sb.AppendLine("-[StackTrace]:");
                sb.AppendLine(ex.StackTrace);
            }

            WriteErrorLog("ERROR", sb.ToString(), caller, file, line);
        }

        public static void Debugger(string message,
         [CallerMemberName] string caller = "",
         [CallerFilePath] string file = "",
         [CallerLineNumber] int line = 0)
        {
            if (!IsDev(_envSettings?.Environment)) return;

            WriteLogs("DEBUG", message, caller, file, line);
           
        }

        private static void WriteLogs(string level,
          string message,
          [CallerMemberName] string caller = "",
          [CallerFilePath] string file = "",
          [CallerLineNumber] int line = 0)
        {
            string? assemblyName = Assembly.GetCallingAssembly().GetName().Name;
            string? fileName = Path.GetFileName(file);
            string? folderName = Path.GetFileName(Path.GetDirectoryName(file));

            Debug.WriteLine(
                $"[{level}]-[{DateTime.Now:MM-dd-yyyy HH:mm:ss}]" +
                $"-[Project:{assemblyName}]" +
                $"-[Object:{caller}]" +
                $"-[Location:{folderName}/{fileName}:{line}]" +
                $"-Message:{message}");
        }

        private static void WriteErrorLog(string level,
          string message,
          [CallerMemberName] string caller = "",
          [CallerFilePath] string file = "",
          [CallerLineNumber] int line = 0)
        {
            string? timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Extract filename and folder
            string? fileName = Path.GetFileName(file);
            string? folderName = Path.GetFileName(Path.GetDirectoryName(file));
            string? assemblyName = Assembly.GetCallingAssembly().GetName().Name;

            Debug.WriteLine($"\n'[{level}]-[Timestamp]: {timestamp}");
            Debug.WriteLine($"-[Project]  : {assemblyName}");
            Debug.WriteLine($"-[Method]   : {caller}");
            Debug.WriteLine($"-[Location] : {folderName}/{fileName}:{line}");
            Debug.WriteLine($"-[Message]  : {message}");
        }

        private static string WrapMessage(string label, string message, int maxLineLength)
        {
            var sb = new StringBuilder();
            var words = message.Split(' ');
            int currentLength = label.Length;

            sb.Append(label);
            foreach (var word in words)
            {
                if (currentLength + word.Length + 1 > maxLineLength)
                {
                    sb.AppendLine();
                    sb.Append("              "); // Indent continuation
                    currentLength = 13; // Indentation length
                }

                sb.Append(word + " ");
                currentLength += word.Length + 1;
            }

            return sb.ToString().TrimEnd();
        }
    }
}
