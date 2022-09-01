using Microsoft.Extensions.Configuration;

namespace JSON_Configuration
{
    public class JSON_Configuration
    {


        private IConfigurationBuilder config;

        private String JFile_name;
        JSON_Configuration(String JFile_name){

            this.JFile_name = JFile_name;
            Object _config = new ConfigurationBuilder();
        }

    }
}