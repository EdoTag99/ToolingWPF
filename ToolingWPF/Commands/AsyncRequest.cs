using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ToolingWPF.Classes;
using ToolingWPF.Model;

namespace ToolingWPF.Commands
{
    /// <summary>
    /// Classe riservata alle richieste al backend
    /// </summary>
    internal class AsyncRequest
    {
        private static readonly string base_url = "http://localhost:8080/Tooling";
        private static readonly HttpClient httpClient = new HttpClient();
        private static ResponseBool response = new ResponseBool();

        public static async Task<ResponseMT> GetStatusMagazine(int MagazineID)
        {
            ResponseMT response = new ResponseMT();
            try
            {
                string json;
                if (MagazineID < 0)
                {
                    json = await httpClient.GetStringAsync(base_url + $"/GetStatusMagazines");
                }
                else
                {
                    json = await httpClient.GetStringAsync(base_url + $"/GetStatusMagazine?MagazineID={MagazineID}");
                }
                response = JsonSerializer.Deserialize<ResponseMT>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            if (response.Data != null)
            {
                Array.Sort(response.Data);
            }
            return response;
        }

        public static async Task<ResponseBool> LoadMagazine()
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + "/LoadMagazine?FileName=C:\\Users\\etagliani\\source\\Recipes\\tools.json");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<ResponseTP> GetStatusPress(int PressId)
        {
            ResponseTP response = new ResponseTP();
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/GetStatusPress?PressId={PressId}");
                response = JsonSerializer.Deserialize<ResponseTP>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<ResponseBool> AddTool(int PressId, int Width, int Position, int MagazineID)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/AddTool?PressId={PressId}&Width={Width}&Position={Position}&MagazineID={MagazineID}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return response;
        }

        public static async Task<ResponseBool> AddToolFirstFreePosition(int PressId, int Width, int MagazineID)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/AddToolFirstFreePosition?PressId={PressId}&Width={Width}&MagazineID={MagazineID}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<ResponseBool> RemoveTool(int PressId, int Width, int Position)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/RemoveTool?PressId={PressId}&Width={Width}&Position={Position}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<ResponseBool> RemoveAllTools(int PressId)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/RemoveAllTools?PressId={PressId}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<List<Recipe>> GetRecipes()
        {
            List<Recipe> response = new List<Recipe>();
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/GetRecipes");
                response = JsonSerializer.Deserialize<List<Recipe>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return response;
        }

        public static async Task<ResponseBool> ImportRecipe(int PressId, Recipe recipe)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/LoadRecipe?PressId={PressId}&Name={recipe.Name}&Source={recipe.Source}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return response;
        }

        public static async Task<ResponseBool> Validation()
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + "/Validation");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<ResponseBool> AddRecipe(string Format, string fileName, string filePath)
        {
            string jsonString;
            StreamReader reader = new StreamReader(filePath);

            jsonString = reader.ReadToEnd();

            try
            {
                HttpClient httpClientN = new HttpClient
                {
                    BaseAddress = new Uri(base_url)
                };
                var data = new Dictionary<string, string>
                    {
                        { "Json", jsonString},
                        { "Source", Format},
                        { "Name", fileName}
                    };

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, base_url + "/NewRecipe")
                {
                    Content = JsonContent.Create(data)
                };
                var responseContent = await httpClient.SendAsync(httpRequestMessage);
                var responseString = await responseContent.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResponseBool>(responseString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return response;
        }

        public static async Task<ResponseBool> SaveBarAsRecipe(int PressId, string format, string name)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/SaveBarAsRecipe?PressId={PressId}&Format={format}&Name={name}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return response;
        }

        public static async Task<ResponseBool> DeleteRecipe(Recipe recipe)
        {
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/DeleteRecipe?Format={recipe.Source}&Name={recipe.Name}");
                response = JsonSerializer.Deserialize<ResponseBool>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return response;
        }

        public static async Task<PressBar[]> GetPressBars()
        {
            PressBar[] pressBars;
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/GetPressBars");
                pressBars = JsonSerializer.Deserialize<PressBar[]>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return pressBars;
        }

        public static async Task<List<int>> GetMagazines()
        {
            int[] magazineIds;
            try
            {
                string json = await httpClient.GetStringAsync(base_url + $"/GetMagazines");
                magazineIds = JsonSerializer.Deserialize<int[]>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return magazineIds.ToList();
        }
    }
}