import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import {
    faBars, faHouseChimney, faCloudShowersHeavy,
    faComment, faAnglesLeft, faMagnifyingGlass, faCloudRain,
    faTemperatureHalf, faUser, faCloudBolt, faSnowflake,
    faSun, faCloud, faSmog, faDroplet, faWind, faCompass,
    faBinoculars, faPercent
} from '@fortawesome/free-solid-svg-icons';

library.add(faBars, faHouseChimney, faCloudShowersHeavy,
    faComment, faAnglesLeft, faMagnifyingGlass, faGoogle,
    faCloudRain, faTemperatureHalf, faUser, faCloudBolt,
    faSnowflake, faSun, faCloud, faSmog, faDroplet, faWind,
    faCompass, faBinoculars, faPercent);
export default FontAwesomeIcon;