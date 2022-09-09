namespace MyBlog.WebAPI.Utility.ApiResult
{
    public class ApiResult
    {

        public int Code { get; set; }

        public string Msg { get; set; }   //错误消息


        public int Total { get; set; }

        public dynamic ?Data { get; set; }  //dynamic动态类型，类似于Object
    }
}
