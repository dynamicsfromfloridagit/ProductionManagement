using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using ProductionManagement.Web.Models;
using System.ComponentModel;
using System.Text.Json;

namespace ProductionManagement.Web.Controllers
{
    public class IotController(BlobServiceClient _blobServiceClient, ILogger<IotController> _logger, IHttpClientFactory _httpClientFactory) : Controller
    {
 
        public async Task<IActionResult> PostBoron(string toggleme)
        {
            _logger.LogInformation("++++++++++++++++ TOGGLE TOGGLE TOGGLE ??");

            using var boronclient = _httpClientFactory.CreateClient("DefaultHttpClient");
            

           //var isLigthOnorOffResponse = await boronclient.GetStringAsync("isLigthOnorOff");
            var responseLightOFFONvalue = await boronclient.GetAsync("isLigthOnorOff");

            // TODO System.Net.Http.HttpRequestException: 'Response status code does not indicate success: 408 (Request Timeout).'
            if (!responseLightOFFONvalue.IsSuccessStatusCode)
            {
                RedirectToAction("Boron");
            }

            var stringresponseLightOFFONvalue = responseLightOFFONvalue.Content.ReadAsStringAsync().Result;


            _logger.LogInformation("+?????????????????? isLigthOnorOffResponse: {isLigthOnorOffResponse}", stringresponseLightOFFONvalue);
           // using var jsonlightoffondocu = JsonDocument.Parse(isLigthOnorOffResponse);
            using var jsonStringResponseLightOffOnDocument = JsonDocument.Parse(stringresponseLightOFFONvalue);
            
            if (jsonStringResponseLightOffOnDocument.RootElement.TryGetProperty("result", out var outresultislight))
            {
                _logger.LogInformation("!!!!!!!! TOGGLE TOGGLE TOGGLE !!!!!!  isLigthOnorOff {outresultislight} ", outresultislight);
                if (outresultislight.ToString().ToLower() == "1")
                {
                    toggleme = "off";
                }
                else 
                {
                    toggleme = "on";
                }
            }

            var content = default(FormUrlEncodedContent);
            var ledPostresponse = default(HttpResponseMessage);
            
            if (toggleme.ToLower() == "on")
            {
                content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("command", "on")
                });

                ledPostresponse = await boronclient.PostAsync("led", content);

            }
            else if (toggleme.ToLower() == "off")
            {
                content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("command", "off")
                });
                ledPostresponse = await boronclient.PostAsync("led", content);
            }
            else {
                _logger.LogInformation("Invalid toggle value: {ToggleValue}. Expected 'on' or 'off'.", toggleme);
                
            }

            if (ledPostresponse.IsSuccessStatusCode)
            {
                _logger.LogInformation("********* TOGGLE TOGGLE TOGGLE ******* Response status code: {Content}", ledPostresponse.Content);
                
                var stringLEDesponse = ledPostresponse.Content.ReadAsStringAsync().Result;
                _logger.LogInformation("####### TOGGLE TOGGLE TOGGLE ######### The STR: {strresponse}", stringLEDesponse);

                // string to JsonDcoument
                using var jsondocu = JsonDocument.Parse(stringLEDesponse);

                if (jsondocu.RootElement.TryGetProperty("result", out var outresult))
                {
                    _logger.LogInformation("!!!!!!!! TOGGLE TOGGLE TOGGLE !!!!!!  result {resultado} ", outresult);
                }
            }
            else
            {
                _logger.LogError("Failed to toggle LED. Status code: {StatusCode}", ledPostresponse.StatusCode);
                // response.Content.Headers.ContentType.MediaType = "application/json";
                // response.Content.Headers.ContentType.CharSet = "utf-8";
                var strresponse = ledPostresponse.Content.ReadAsStringAsync().Result;
                _logger.LogError("####### ... TOGGLE ERROR ERROR ... ######### The STR: {strresponse}", strresponse);
            }

            return RedirectToAction("Boron");
        }


        public async Task<IActionResult> Boron()
        {
            //using var boronclient = _httpClientFactory.CreateClient();
            using var boronclient = _httpClientFactory.CreateClient("DefaultHttpClient");
            

            var responseAnalogValue = await boronclient.GetAsync("analogvalue");
            _logger.LogInformation("++++++++++++++++ Response status code: {StatusCode}", responseAnalogValue.StatusCode);
           
                //var isLigthOnorOffResponse = await boronclient.GetStringAsync("isLigthOnorOff");
                var responseLightOFFONvalue = await boronclient.GetAsync("isLigthOnorOff");

            _logger.LogInformation("+?????????????????? isLigthOnorOffResponse: {responseLightOFFONvalue}", responseLightOFFONvalue);
            //using var jsonlightoffondocu = JsonDocument.Parse(isLigthOnorOffResponse);


            var stringANALOGresponse = responseAnalogValue.Content.ReadAsStringAsync().Result;
            var stringresponseLightOFFONvalue = responseLightOFFONvalue.Content.ReadAsStringAsync().Result;
            var responseWasSuccess = responseAnalogValue.IsSuccessStatusCode;

            if (responseAnalogValue.IsSuccessStatusCode) {
                _logger.LogInformation("**************** Response status code: {Content}", responseAnalogValue.Content);
                
                _logger.LogInformation("################ The STR: {strresponse}", stringANALOGresponse);

                // string to JsonDcoument
                using var jsondocu = JsonDocument.Parse(stringANALOGresponse);
                using var jsonlightoffondocument = JsonDocument.Parse(stringresponseLightOFFONvalue);

                var isonoroff = "0";
                var resultadoAnalog = "0";
                if (jsondocu.RootElement.TryGetProperty("result", out var outresult))
                {
                    _logger.LogInformation("!!!!!!!!!!!!!!  result {resultado} ", outresult);

                    resultadoAnalog = outresult.ToString();
                        // light?

                        if (jsonlightoffondocument.RootElement.TryGetProperty("result", out var outresultislight))
                    {
                        
                        _logger.LogInformation("!!!!!!!! TOGGLE TOGGLE TOGGLE !!!!!!  isLigthOnorOff {outresultislight} ", outresultislight);
                        if (outresultislight.ToString().ToLower() == "1")
                        {
                            isonoroff = "1";
                        }
                        else
                        {
                            isonoroff = "0";
                        }

                    }

                    // light?
                    return View(new
                    {
                        isThereResponse = responseWasSuccess,
                        responseContent = stringANALOGresponse,
                        theresultadoAnalog = resultadoAnalog,
                        IsOnorOff = isonoroff
                    });
                }

                

                    return View(new { 
                    isThereResponse = responseAnalogValue.IsSuccessStatusCode,
                    responseContent = stringANALOGresponse


                });
            } 
            else
            {
                return View(new { isThereResponse = responseAnalogValue.IsSuccessStatusCode, responseContent = stringANALOGresponse });
            }
            
            
        }


        public async Task<IActionResult> Index()
        {
            
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("4boron2humi-tempe-container");
            // todoContainer.GetBlobsAsync()

            var blobs = blobContainerClient.GetBlobsAsync();

            List<string> nameOfBlobList = new List<string>();
            await foreach (var blobItemX in blobs)
            {
                nameOfBlobList.Add(blobItemX.Name);
                var justname = blobItemX.Name.Split('.').FirstOrDefault();
                _logger.LogInformation("Blob name: {BlobName}", justname);
                /*
                var datetimeArray = justname.Split('/');
                var dateonly = new DateOnly(int.Parse( datetimeArray[2]),int.Parse( datetimeArray[3]), int.Parse(datetimeArray[4]));
                var timeonly = new TimeOnly(int.Parse(datetimeArray[5]), int.Parse(datetimeArray[6]));
                // TODO: Extract timestamp from blob name if it follows a specific pattern, e.g., "data_2024-06-01T12-00-00.json"
                var timeanddate = new DateTime(dateonly, timeonly);
                */
            }

            // getting name of last blob saved in container
            var lastorfirstname = nameOfBlobList.LastOrDefault();

            // getting last blob of container
            var oneBlobClient = blobContainerClient.GetBlobClient(lastorfirstname);

            var JsonElementsList = new List<JsonElement>();
            var measureDataList = new List<MeasuredData>();
            var bodyDataList = new List<Body>();
            var moreDataList = new List<MoreData>();
            try
            {
                _logger.LogInformation("========    Starting deserialization of blob content    ========");

                // Download blob to stream
                using var downloadStream = new MemoryStream();
                await oneBlobClient.DownloadToAsync(downloadStream);
                downloadStream.Position = 0;

                using var reader = new StreamReader(downloadStream);
                string content = reader.ReadToEnd().Trim();

                // sets of data (humidity, Temperature) are saved in a Blob separated by new lines or returns
                foreach (var line in content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _logger.LogInformation("Deserialized line: {LineContent}", line);
                    using var lineDoc = JsonDocument.Parse(line);
                    JsonElementsList.Add(lineDoc.RootElement.Clone());

                    if (lineDoc.RootElement.TryGetProperty("Body", out var bodyElement))
                    {
                        _logger.LogInformation("......................Found 'data' element in line............");
                        _logger.LogInformation("Extracted 'data' element: {DataContent}", bodyElement.GetRawText());
                        var elebody = bodyElement.Clone();
                        var bodyClass = JsonSerializer.Deserialize<Body>(elebody);
                        bodyDataList.Add(bodyClass);

                        var measureData = JsonSerializer.Deserialize<MeasuredData>(bodyClass.data);
                        measureDataList.Add(measureData);
                        var moredatax = new MoreData
                        {
                            Body = bodyClass,
                            MeasuredData = measureData
                        };
                        moreDataList.Add(moredatax);
                    }
                    else
                    {
                        _logger.LogInformation("+++++++++++++++++++++++++++++++++>>>>>>>>No 'data' element found in line.");
                    }


                    
                }

                _logger.LogInformation("========    DONE deserialization of eledataString content    ========");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "========>>>>>> Error deserializing blob content");
            }

            return View(moreDataList);
        }
    }


    // ========== 



    // ======== 
}
