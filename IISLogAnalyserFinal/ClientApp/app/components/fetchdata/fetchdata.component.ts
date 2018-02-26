import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    //public forecasts: WeatherForecast[];

    public userdatas: UserData[];
    public hitsData: HitsData[];

    //constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
    //    http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
    //        this.forecasts = result.json() as WeatherForecast[];
    //    }, error => console.error(error));
    //}

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/UserDatas').subscribe(result => {
            debugger;
            this.userdatas = result.json() as UserData[];
            this.hitsData = JSON.parse(result.json().slice(-1)[0].jsonText);
        }, error => console.error(error));
    }
}
interface UserData {
    id: number;
    jsonText: string;
}

interface HitsData {
    IP: string;
    FQDN: string;
    Hits: number;
}
//interface WeatherForecast {
//    dateFormatted: string;
//    temperatureC: number;
//    temperatureF: number;
//    summary: string;
//}
