using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PSTDotNetTrainingBatch5.BaganMapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaganMap : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string folderPath = "Data/BaganMap.json"!;
            var jsonStr = System.IO.File.ReadAllText(folderPath);

            var result = JsonConvert.DeserializeObject<BaganMapResponseModel>(jsonStr)!;

            var lst = result.Tbl_BaganMapInfoDetailData.ToList();
            return Ok(lst);
        }

        [HttpGet ("{id}")]
        public IActionResult GetBlog(string id)
        {
            string folderPath = "Data/BaganMap.json"!;
            var jsonStr = System.IO.File.ReadAllText(folderPath);

            var result = JsonConvert.DeserializeObject<BaganMapResponseModel>(jsonStr)!;

            var item = result.Tbl_BaganMapInfoDetailData
                .FirstOrDefault(x => x.Id == id);
            if (item == null) return BadRequest("No data found.");

            return Ok(item);
        }
    }
    public class BaganMapResponseModel
    {
        public Tbl_Baganmapinfodata[] Tbl_BaganMapInfoData { get; set; }
        public Tbl_Baganmapinfodetaildata[] Tbl_BaganMapInfoDetailData { get; set; }
        public Tbl_Travelroutelistdata[] Tbl_TravelRouteListData { get; set; }
    }

    public class Tbl_Baganmapinfodata
    {
        public string Id { get; set; }
        public string PagodaMmName { get; set; }
        public string PagodaEngName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class Tbl_Baganmapinfodetaildata
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class Tbl_Travelroutelistdata
    {
        public string TravelRouteId { get; set; }
        public string TravelRouteName { get; set; }
        public string TravelRouteDescription { get; set; }
        public string[] PagodaList { get; set; }
    }

}
