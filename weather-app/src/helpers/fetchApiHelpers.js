const MAPBOX_BASE = 'https://api.mapbox.com/geocoding/v5/mapbox.places/';
const OPENWEATHER_BASE = 'https://localhost:44324/v1/weather';

const getGeocode = async (location) => {
  const URL = `${MAPBOX_BASE}${location}.json?types=place&access_token=${process.env.REACT_APP_MAP_TOKEN}`;

  const geocodeList = await fetch(URL).then(data => data.json()).then(result => result.features);
  const coordinates = geocodeList[0].center;
  const placeName = geocodeList[0].matching_text ? geocodeList[0].matching_text : geocodeList[0].text;
  const state = geocodeList[0].context[0].text;
  const country = geocodeList[0].context[1].text;
  return { coordinates, placeName, state, country };
}

// Could get lat/long and use in openweatherapi call to chain the requests
export const getWeather = async (location) => {
  const geocodeResult = await getGeocode(location);
  const placeName = {
    city: geocodeResult.placeName.split(/\b\s[Ss]hi\b/)[0], // remove city suffix
    state: geocodeResult.state,
    country: geocodeResult.country
  }

  let currentTemp;
  let weatherMain;
  let tempMax;
  let tempMin; 

  try {
    const URL = `${OPENWEATHER_BASE}?city=${location}`;//&appid=${process.env.REACT_APP_OW_API_KEY}`;
    const weatherResult = await fetch(URL).then(data => data.json()).then(result => result);
    currentTemp = weatherResult.main.temp;
    weatherMain = weatherResult.weather[0].main;
    tempMax = weatherResult.main.temp_max;
    tempMin = weatherResult.main.temp_min;
  } catch (error) {
    if(error.message.includes('temp')){
      console.log(error.message);
      currentTemp = 0;
      tempMax = 0;
      tempMin = 0;
      alert('Invalid location, try again!');
    } else {
      alert('Error while trying to fetch weather info');
    }
  }
  
  return [{ currentTemp, weatherMain, tempMax, tempMin }, placeName];
}