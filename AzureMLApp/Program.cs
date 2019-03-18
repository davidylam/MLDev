// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallRequestResponseService
{
    class Program
    {
        static void Main(string[] args)
        {
            InvokeRequestResponseService().Wait();
        }

        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>> () {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "ID", "1"
                                            },
                                            {
                                                "target", "1"
                                            },
                                            {
                                                "Gender", "F"
                                            },
                                            {
                                                "EngineHP", "522"
                                            },
                                            {
                                                "credit_history", "656"
                                            },
                                            {
                                                "Years_Experience", "1"
                                            },
                                            {
                                                "annual_claims", "0"
                                            },
                                            {
                                                "Marital_Status", "Married"
                                            },
                                            {
                                                "Vehical_type", "Car"
                                            },
                                            {
                                                "Miles_driven_annually", "14749"
                                            },
                                            {
                                                "size_of_family", "5"
                                            },
                                            {
                                                "Age_bucket", "<18"
                                            },
                                            {
                                                "EngineHP_bucket", ">350"
                                            },
                                            {
                                                "Years_Experience_bucket", "<3"
                                            },
                                            {
                                                "Miles_driven_annually_bucket", "<15k"
                                            },
                                            {
                                                "credit_history_bucket", "Fair"
                                            },
                                            {
                                                "State", "IL"
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>() {
                    }
                };

                const string apiKey = "4VWB0ThhAxCa/S8yYa03weoKobkoxbzC2xIxQtlXlkqNMDyRD2EO5uJ3xxSr6FsNC5DPhvhesiuVLGU9LCTk3Q=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/b49139874c394b8493f82d9c2de6ab1b/services/ad41bd50517441c9bb8dd96fddc44e8a/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}