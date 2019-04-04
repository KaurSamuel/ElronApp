using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using SharpTrooper.Entities;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SharpTrooper.Core
{
    public class SharpTrooperCore
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private string apiUrl = "http://swapi.co/api";
        private string _proxyName = null;

        public SharpTrooperCore()
        {
        }

        public SharpTrooperCore(string proxyName)
        {
            _proxyName = proxyName;
        }

        #region Private

        private async Task<string> Request(string url, HttpMethod httpMethod)
        {

            var request=await Request(url, httpMethod, null, false);
            return request;
        }

        private async Task<string> Request(string url, HttpMethod httpMethod, string data, bool isProxyEnabled)
        {
            string result = string.Empty;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = httpMethod.ToString();

            if (!String.IsNullOrEmpty(_proxyName))
            {
                httpWebRequest.Proxy = new WebProxy(_proxyName, 80);
                httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            if (data != null)
            {
                byte[] bytes = UTF8Encoding.UTF8.GetBytes(data.ToString());
                httpWebRequest.ContentLength = bytes.Length;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Dispose();
            }

            try
            { 
                var responseAsync= await httpWebRequest.GetResponseAsync();
                HttpWebResponse httpWebResponse = (HttpWebResponse)responseAsync;

                StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Dispose();
            }
            catch (WebException ex)
            {
            }

            return result;
        }


        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }

                return parameters.Remove(parameters.Length - 1, 1).ToString();
        }

        private T GetSingle<T>(string endpoint, Dictionary<string, string> parameters = null) where T : SharpEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            return GetSingleByUrl<T>(url: string.Format("{0}{1}{2}", apiUrl, endpoint, serializedParameters)).Result;
        }

        private SharpEntityResults<T> GetMultiple<T>(string endpoint) where T : SharpEntity
        {
            return (GetMultiple<T>(endpoint, null)).Result;
        }

        private async Task<SharpEntityResults<T>> GetMultiple<T>(string endpoint, Dictionary<string, string> parameters) where T : SharpEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            string json = await Request(string.Format("{0}{1}{2}", apiUrl, endpoint, serializedParameters), HttpMethod.GET);
            SharpEntityResults<T> swapiResponse = JsonConvert.DeserializeObject<SharpEntityResults<T>>(json);
            return swapiResponse;
        }

        private NameValueCollection GetQueryParameters(string dataWithQuery)
        {
            NameValueCollection result = new NameValueCollection();
            string[] parts = dataWithQuery.Split('?');
            if (parts.Length > 0)
            {
                string QueryParameter = parts.Length > 1 ? parts[1] : parts[0];
                if (!string.IsNullOrEmpty(QueryParameter))
                {
                    string[] p = QueryParameter.Split('&');
                    foreach (string s in p)
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            string[] temp = s.Split('=');
                            result.Add(temp[0], temp[1]);
                        }
                        else
                        {
                            result.Add(s, string.Empty);
                        }
                    }
                }
            }
            return result;
        }

        private async Task<SharpEntityResults<T>> GetAllPaginated<T>(string entityName, string pageNumber = "1") where T : SharpEntity
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("page", pageNumber);

            SharpEntityResults<T> result = await GetMultiple<T>(entityName, parameters);

            result.nextPageNo = String.IsNullOrEmpty(result.next) ? null : GetQueryParameters(result.next)["page"];
            result.previousPageNo = String.IsNullOrEmpty(result.previous) ? null : GetQueryParameters(result.previous)["page"];

            return result;
        }

        #endregion

        #region Public

        /// <summary>
        /// get a specific resource by url
        /// </summary>
        public async Task<T> GetSingleByUrl<T>(string url) where T : SharpEntity
        {
            string json = await Request(url, HttpMethod.GET);
            T swapiResponse = JsonConvert.DeserializeObject<T>(json);
            return swapiResponse;
        }

        // People
        /// <summary>
        /// get a specific people resource
        /// </summary>
        public People GetPeople(string id)
        {
            return GetSingle<People>("/people/" + id);
        }

        /// <summary>
        /// get all the people resources
        /// </summary>
        public async Task <SharpEntityResults<People>> GetAllPeople(string pageNumber = "1")
        {
            SharpEntityResults<People> result = await GetAllPaginated<People>("/people/", pageNumber);

            return result;
        }

        // Film
        /// <summary>
        /// get a specific film resource
        /// </summary>
        public Film GetFilm(string id)
        {
            return GetSingle<Film>("/films/" + id);
        }

        /// <summary>
        /// get all the film resources
        /// </summary>
        public async Task<SharpEntityResults<Film>> GetAllFilms(string pageNumber = "1")
        {
            SharpEntityResults<Film> result = await GetAllPaginated<Film>("/films/", pageNumber);

            return result;
        }

        // Planet
        /// <summary>
        /// get a specific planet resource
        /// </summary>
        public Planet GetPlanet(string id)
        {
            return GetSingle<Planet>("/planets/" + id);
        }

        /// <summary>
        /// get all the planet resources
        /// </summary>
        public async Task<SharpEntityResults<Planet>> GetAllPlanets(string pageNumber = "1")
        {
            SharpEntityResults<Planet> result = await GetAllPaginated<Planet>("/planets/", pageNumber);
            return result;
        }

        // Specie
        /// <summary>
        /// get a specific specie resource
        /// </summary>
        public Specie GetSpecie(string id)
        {
            return GetSingle<Specie>("/species/" + id);
        }

        /// <summary>
        /// get all the specie resources
        /// </summary>
        public async Task<SharpEntityResults<Specie>> GetAllSpecies(string pageNumber = "1")
        {
            SharpEntityResults<Specie> result =await GetAllPaginated<Specie>("/species/", pageNumber);

            return result;
        }

        // Starship
        /// <summary>
        /// get a specific starship resource
        /// </summary>
        public Starship GetStarship(string id)
        {
            return GetSingle<Starship>("/starships/" + id);
        }

        /// <summary>
        /// get all the starship resources
        /// </summary>
        public async Task<SharpEntityResults<Starship>> GetAllStarships(string pageNumber = "1")
        {
            SharpEntityResults<Starship> result =await GetAllPaginated<Starship>("/starships/", pageNumber);

            return result;
        }

        // Vehicle
        /// <summary>
        /// get a specific vehicle resource
        /// </summary>
        public Vehicle GetVehicle(string id)
        {
            return GetSingle<Vehicle>("/vehicles/" + id);
        }

        /// <summary>
        /// get all the vehicle resources
        /// </summary>
        public async Task<SharpEntityResults<Vehicle>> GetAllVehicles(string pageNumber = "1")
        {
            SharpEntityResults<Vehicle> result = await GetAllPaginated<Vehicle>("/vehicles/", pageNumber);

            return result;
        }

        #endregion

        public void GetDictionaryOfItem(SortedDictionary<string,object> dictionary)
        {
            foreach (var item in dictionary)
            {
                string itemKey = item.Key;

                if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url" || itemKey == "title" || itemKey == "vehicle_class")
                    continue;
                itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                itemStrings.PropertyNames.Add(itemKey);

                if (item.Value == null)
                {
                    itemStrings.PropertyValues.Add("unknown");
                }
                else if (item.Value.GetType() == typeof(List<string>))
                {

                    List<string> stringList = new List<string>();

                    foreach (var listItem in (List<string>)item.Value)
                    {
                        string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                        category = char.ToUpper(category[0]) + category.Substring(1);
                        switch (category)
                        {
                            case ("Planets"):
                                Planet planetResult = await core.GetSingleByUrl<Planet>(listItem);
                                stringList.Add(planetResult.name);
                                break;
                            case ("People"):
                                People peopleResult = await core.GetSingleByUrl<People>(listItem);
                                stringList.Add(peopleResult.name);
                                break;
                            case ("Films"):
                                Film filmResult = await core.GetSingleByUrl<Film>(listItem);
                                stringList.Add(filmResult.title);
                                break;
                            case ("Species"):
                                Specie speciesResult = await core.GetSingleByUrl<Specie>(listItem);
                                stringList.Add(speciesResult.name);
                                break;
                            case ("Starships"):
                                Starship starshipResult = await core.GetSingleByUrl<Starship>(listItem);
                                stringList.Add(starshipResult.name);
                                break;
                            case ("Vehicles"):
                                Vehicle vehicleResult = await core.GetSingleByUrl<Vehicle>(listItem);
                                stringList.Add(vehicleResult.name);
                                break;
                        }
                    }
                    string listToString = string.Join(", ", stringList);

                    if (stringList.Count() == 0)
                        listToString = "unknown";

                    itemStrings.PropertyValues.Add(listToString);
                }
                else if (item.Value.ToString().StartsWith("https://swapi.co/api/"))
                {
                    string category = item.Value.ToString().Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                    category = char.ToUpper(category[0]) + category.Substring(1);
                    switch (category)
                    {
                        case ("Planets"):
                            Planet planetResult = await core.GetSingleByUrl<Planet>(item.Value.ToString());
                            itemStrings.PropertyValues.Add(planetResult.name);
                            break;
                        case ("People"):
                            People peopleResult = await core.GetSingleByUrl<People>(item.Value.ToString());
                            itemStrings.PropertyValues.Add(peopleResult.name);
                            break;
                        case ("Films"):
                            Film filmResult = await core.GetSingleByUrl<Film>(item.Value.ToString());
                            itemStrings.PropertyValues.Add(filmResult.title);
                            break;
                        case ("Species"):
                            Specie speciesResult = await core.GetSingleByUrl<Specie>(item.Value.ToString());
                            itemStrings.PropertyValues.Add(speciesResult.name);
                            break;
                        case ("Starships"):
                            Starship starshipResult = await core.GetSingleByUrl<Starship>(item.Value.ToString());
                            itemStrings.PropertyValues.Add(starshipResult.name);
                            break;
                        case ("Vehicles"):
                            Vehicle vehicleResult = await core.GetSingleByUrl<Vehicle>(item.Value.ToString());
                            itemStrings.PropertyValues.Add(vehicleResult.name);
                            break;
                    }
                }
                else
                {
                    itemStrings.PropertyValues.Add(item.Value.ToString());
                }
            }
        }
    }
}
