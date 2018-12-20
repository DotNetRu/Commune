import { Pipe, PipeTransform } from "@angular/core";
import { City } from "./enums";

@Pipe({ name: "cityName" })
export class CityNamePipe implements PipeTransform {
    public transform(city: City): string {
        switch (city) {
            case City.Spb:
                return "Санкт-Петербург";
            case City.Msk:
                return "Москва";
            case City.Sar:
                return "Саратов";
            case City.Kry:
                return "Красноярск";
            case City.Kzn:
                return "Казань";
            case City.Nsk:
                return "Новосибирск";
        }

        const exhaustingCheck: never = city;
    }
}
