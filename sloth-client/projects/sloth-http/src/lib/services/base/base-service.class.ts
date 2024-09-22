import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse } from "@angular/common/http";
import { inject } from "@angular/core";
import { HttpConfigService } from "../../config/http-config.service";
import { ServiceReturnValue } from "../../models/base/service-return.model";
import { catchError, lastValueFrom, map, Observable, throwError } from "rxjs";

export abstract class BaseService {
    constructor(_controller: string){
        this.controller = _controller;
    }

    protected controller: string = '';
    private httpClient = inject(HttpClient);
    private httpConfig = inject(HttpConfigService);
    private getUrl = (action: string) => `${this.httpConfig.apiUrl}/${this.controller}/${action}`;

    protected async get<T>(action: string, query?: any): Promise<ServiceReturnValue<T>> {        
        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.get<T>(url, {
                    params: query ? new HttpParams({ fromObject: query }) : undefined, 
                    observe: 'response' // Get full HttpResponse
                }).pipe(
                    // Map the response in case of success
                    map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
    
                    // Catch and handle the error
                    catchError((error: HttpErrorResponse) => {
                        const mappedError = this.mapError<T>(error);
                        // Re-throw the error to propagate it as a rejected promise
                        return throwError(() => mappedError);
                    })
                ));
            return result;
        }
        catch (error) {
            // Return the error as a ServiceReturnValue
            return error as ServiceReturnValue<T>;
        }
    }

    protected async post<T>(action: string, query: any): Promise<ServiceReturnValue<T>> {
        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.post<T>(url, query, {
                    observe: 'response' // Get full HttpResponse
                }).pipe(
                    // Map the response in case of success
                    map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
    
                    // Catch and handle the error
                    catchError((error: HttpErrorResponse) => {
                        const mappedError = this.mapError<T>(error);
                        // Re-throw the error to propagate it as a rejected promise
                        return throwError(() => mappedError);
                    })
                ));
            return result;
        }
        catch (error) {
            // Return the error as a ServiceReturnValue
            return error as ServiceReturnValue<T>;
        }
    }

    protected async put<T>(action: string, query: any): Promise<ServiceReturnValue<T>> {
        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.put<T>(url, query, {
                    observe: 'response' // Get full HttpResponse
                }).pipe(
                    // Map the response in case of success
                    map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
    
                    // Catch and handle the error
                    catchError((error: HttpErrorResponse) => {
                        const mappedError = this.mapError<T>(error);
                        // Re-throw the error to propagate it as a rejected promise
                        return throwError(() => mappedError);
                    })
                ));
            return result;
        }
        catch (error) {
            // Return the error as a ServiceReturnValue
            return error as ServiceReturnValue<T>;
        }
    }

    protected async delete<T>(action: string, query?: any): Promise<ServiceReturnValue<T>> {
        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.delete<T>(url, {
                    params: query ? new HttpParams({ fromObject: query }) : undefined, 
                    observe: 'response' // Get full HttpResponse
                }).pipe(
                    // Map the response in case of success
                    map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
    
                    // Catch and handle the error
                    catchError((error: HttpErrorResponse) => {
                        const mappedError = this.mapError<T>(error);
                        // Re-throw the error to propagate it as a rejected promise
                        return throwError(() => mappedError);
                    })
                ));
            return result;
        }
        catch (error) {
            // Return the error as a ServiceReturnValue
            return error as ServiceReturnValue<T>;
        }
    }

    protected async patch<T>(action: string, query: any): Promise<ServiceReturnValue<T>> {
        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.patch<T>(url, query, {
                    observe: 'response' // Get full HttpResponse
                }).pipe(
                    // Map the response in case of success
                    map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
    
                    // Catch and handle the error
                    catchError((error: HttpErrorResponse) => {
                        const mappedError = this.mapError<T>(error);
                        // Re-throw the error to propagate it as a rejected promise
                        return throwError(() => mappedError);
                    })
                ));
            return result;
        }
        catch (error) {
            // Return the error as a ServiceReturnValue
            return error as ServiceReturnValue<T>;
        }
    }

    protected getRaw<T>(action: string, query?: any): Observable<T> {        
        var url = this.getUrl(action);
        return this.httpClient.get<T>(url, {
            params: query ? new HttpParams({ fromObject: query }) : undefined
        })
    }

    protected postRaw<T>(action: string, query: any): Observable<T> {
        var url = this.getUrl(action);
        return this.httpClient.post<T>(url, query);
    }

    protected putRaw<T>(action: string, query: any): Observable<T> {
        var url = this.getUrl(action);
        return this.httpClient.put<T>(url, query);
    }

    protected deleteRaw<T>(action: string, query?: any): Observable<T> {
        var url = this.getUrl(action);
        return this.httpClient.delete<T>(url, {
            params: query ? new HttpParams({ fromObject: query }) : undefined
        });
    }

    protected patchRaw<T>(action: string, query: any): Observable<T> {
        var url = this.getUrl(action);
        return this.httpClient.patch<T>(url, query);
    }

    private mapResponse<T>(response: HttpResponse<T>): ServiceReturnValue<T> {
        return {
            data: response.body as T,
            responseCode: response.status,
            error: null,
            success: true
        }
    }

    private mapError<T>(error: HttpErrorResponse): ServiceReturnValue<T> {
        return {
            data: null as any,
            responseCode: error.status || 500,
            error: error.message || 'An unknown error occurred',
            success: false
        }
    }
}