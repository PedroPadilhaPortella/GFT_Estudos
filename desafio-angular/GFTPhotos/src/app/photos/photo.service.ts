import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Photo } from "./photo";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class PhotoService {

    private API = environment.photosUrl;

    constructor(private http: HttpClient) { }

    getPhotos():Observable<Photo[]>
    {
        return this.http.get<Photo[]>(this.API);
    }
}
