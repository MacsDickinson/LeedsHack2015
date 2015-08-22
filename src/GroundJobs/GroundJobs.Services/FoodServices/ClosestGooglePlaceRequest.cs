using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class ClosestGooglePlaceRequest : IServiceRequest<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; set; }
        public string APIKey { get; set; }
        public string PostCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public List<EateryType> Types { get; set; }
        public List<string> Names { get; set; }
        public string Keyword { get; set; }

        public List<ClosestGooglePlaceResponse> Get()
        {
            var closestPlaces = new List<ClosestGooglePlaceResponse>();

            if (!string.IsNullOrEmpty(PostCode))
            {
                var postcodeData = GetResponseString($"http://api.postcodes.io/postcodes/{PostCode}");
                postcodeData.Wait();
                var postcode = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(postcodeData.Result);

                Latitude = float.Parse(postcode.result.latitude.ToString());
                Longitude = float.Parse(postcode.result.longitude.ToString());
            }

            var types = Types.Aggregate("", (current, type) => current + (type + "|"));
            var names = Names.Aggregate("", (current, type) => current + (type + " "));

            var googlePlacesResponse = GetResponseString($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={Latitude},{Longitude}&radius=500&types={types}&name={names}&keyword={Keyword}&key={APIKey}");
            googlePlacesResponse.Wait();
            var googlePlaces = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(googlePlacesResponse.Result);

            if (googlePlaces.results.Count <= 0) return closestPlaces;

            foreach (var googlePlace in googlePlaces.results)
            {
                var closestPlace = new ClosestGooglePlaceResponse
                {
                    LocationName = googlePlace.name?.ToString(),
                    Latitude = float.Parse(googlePlace.geometry.location.lat.ToString()),
                    Longitude = float.Parse(googlePlace.geometry.location.lng.ToString()),
                };
                closestPlace.SetDistanceFrom(Latitude, Longitude);

                closestPlaces.Add(closestPlace);
            }

            return closestPlaces;
        }
        private static async Task<string> GetResponseString(string url)
        {
            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            return await client.GetStringAsync(url);
        }
    }

    public enum EateryType
    {
        accounting,
        airport,
        amusement_park,
        aquarium,
        art_gallery,
        atm,
        bakery,
        bank,
        bar,
        beauty_salon,
        bicycle_store,
        book_store,
        bowling_alley,
        bus_station,
        cafe,
        campground,
        car_dealer,
        car_rental,
        car_repair,
        car_wash,
        casino,
        cemetery,
        church,
        city_hall,
        clothing_store,
        convenience_store,
        courthouse,
        dentist,
        department_store,
        doctor,
        electrician,
        electronics_store,
        embassy,
        establishment,
        finance,
        fire_station,
        florist,
        food,
        funeral_home,
        furniture_store,
        gas_station,
        general_contractor,
        grocery_or_supermarket,
        gym,
        hair_care,
        hardware_store,
        health,
        hindu_temple,
        home_goods_store,
        hospital,
        insurance_agency,
        jewelry_store,
        laundry,
        lawyer,
        library,
        liquor_store,
        local_government_office,
        locksmith,
        lodging,
        meal_delivery,
        meal_takeaway,
        mosque,
        movie_rental,
        movie_theater,
        moving_company,
        museum,
        night_club,
        painter,
        park,
        parking,
        pet_store,
        pharmacy,
        physiotherapist,
        place_of_worship,
        plumber,
        police,
        post_office,
        real_estate_agency,
        restaurant,
        roofing_contractor,
        rv_park,
        school,
        shoe_store,
        shopping_mall,
        spa,
        stadium,
        storage,
        store,
        subway_station,
        synagogue,
        taxi_stand,
        train_station,
        travel_agency,
        university,
        veterinary_care,
        zoo,
    }
    
}
