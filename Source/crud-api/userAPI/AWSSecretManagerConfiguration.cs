using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Amazon;
using System.Text.Json;

namespace userAPI
{
	public static class AWSSecretManagerConfiguration
	{
        public static async Task<string> GetSecret()
        {
                string secretName = "prod/mssql";
                string region = "us-east-2";

                IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

                GetSecretValueRequest request = new GetSecretValueRequest
                {
                    SecretId = secretName,
                    VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
                };

                GetSecretValueResponse response;

                try
                {
                    response = await client.GetSecretValueAsync(request);
                }
                catch (Exception e)
                {
                    // For a list of the exceptions thrown, see
                    // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                    throw e;
                }

                string secret1 = response.SecretString;
                var secret = JsonSerializer.Deserialize <IDictionary<string, string>>(secret1);

            // Your code goes here
            return secret.Values.First();
        }
    }
}

