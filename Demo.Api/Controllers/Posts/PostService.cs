using Microsoft.Extensions.Options;

namespace Demo.Api.Controllers.Posts
{
	public interface IPostService
	{
		PostOptions GetPostOptions();
	}
	public class PostService: IPostService
	{
		public readonly PostOptions _options;
        public PostService(IOptions<PostOptions> options)
        {
            _options = options.Value;
        }
		public PostOptions GetPostOptions() => _options;
	}

	public class PostOptions
	{
		public TypeInfo Type { get; set; }
		public NumberInfo Number { get; set; }
        public PostOptions()
        {
			Type = new TypeInfo();
			Number = new NumberInfo();
        }
    }

	public class TypeInfo
	{
        public TypeInfo()
        {
			Type1 = "Type1";
			Type2 = "Type2";
        }
        public string Type1 { get; set; }
		public string Type2 { get; set; }
	}
	public class NumberInfo
	{
		public int Num1 { get; set; }
		public int Num2 { get; set; }
        public NumberInfo()
        {
			Num1 = 1;
			Num2 = 2;
        }
    }
}
