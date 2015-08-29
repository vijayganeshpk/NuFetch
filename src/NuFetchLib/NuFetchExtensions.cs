using Newtonsoft.Json;

namespace NuFetchLib {
    public static class NuFetchExtensions {
        public static string ToJson( this object val ) {
            var settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject( val, Formatting.Indented, settings );
        }
    }
}