import styled from 'styled-components';
import morning from '../../media/morning.gif';
import sunset from '../../media/sunset.gif';
import night from '../../media/night.gif';

const StyledWeather = styled.div`
  background-image: url(
    ${props => {
    if (props.bgImage === 'morning') {
      return morning;
    }
    if (props.bgImage === 'sunset') {
      return sunset;
    }
    if (props.bgImage === 'night') {
      return night;
    }
  }}
  );
  background-size: cover;
  background-position: top center;
`

export default StyledWeather;