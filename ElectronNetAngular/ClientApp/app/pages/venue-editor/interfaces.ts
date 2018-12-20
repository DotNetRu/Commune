import { City } from "@dotnetru/shared/city-select";

export interface IVenue {
    id: string;
    city: City;
    name: string;
    address: string;
    mapUrl: string;
}
