import Axios, { AxiosInstance } from 'axios';
const host = "https://vapi.vnappmob.com";

class ProvinceApi{
  GetProvince(){
    const result = Axios.get(`${host}/api/province/`);
    return result;
  }

  GetDistrict(provinceId){
    const result = Axios.get(`${host}/api/province/district/${provinceId}`);
    return result;
  }

  GetWard(districtId){
    const result = Axios.get(`${host}/api/province/ward/${districtId}`);
    return result;
  }
}

export default new ProvinceApi();