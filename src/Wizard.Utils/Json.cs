using System.Text.Encodings.Web;
using System.Text.Json;

namespace wizard.Utils;

public static class Json
{
    public static readonly JsonSerializerOptions SerializerOptions = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };    
} 
