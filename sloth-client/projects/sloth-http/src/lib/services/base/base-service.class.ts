import { HttpClient, HttpParams } from "@angular/common/http";
import { inject } from "@angular/core";
import { HttpConfigService } from "../../config/http-config.service";

export abstract class BaseService {
    constructor(_controller: string){
        this.controller = _controller;
    }

    protected controller: string = '';
    private httpClient = inject(HttpClient);
    private httpConfig = inject(HttpConfigService);
    private getUrl = (action: string) => `${this.httpConfig.apiUrl}/${this.controller}/${action}`;

    protected async get<T>(action: string, query?: any): Promise<T | undefined> {
        // Check if the controller is null, undefined, or an empty string
        if (!this.controller || this.controller.trim() === '') {
            return Promise.reject('Controller is not defined or is empty');
        }
        
        var url = this.getUrl(action);
        // Handle the case where query parameters are provided
        if(query) {
            const params = new HttpParams({ fromObject: query });
            return this.httpClient.get<T>(url, { params }).toPromise();
        } else {
            return this.httpClient.get<T>(url).toPromise();
        }
    }
}