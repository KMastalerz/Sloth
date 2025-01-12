import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse } from "@angular/common/http";
import { catchError, lastValueFrom, map, Observable, of, throwError } from "rxjs";
import { inject } from "@angular/core";

import { FormDataService } from "sloth-utilities";
import { HttpConfigService } from "../config/http-config/http-config.service";
import { ServiceReturnValue } from "../dto/base/service-return-value.model";

export abstract class BaseService {
    constructor(private controller: string){}
    private formDataService = inject(FormDataService);
    private httpClient = inject(HttpClient);
    private httpConfig = inject(HttpConfigService);
    private getUrl = (action: string) => `${this.httpConfig.apiUrl}/${this.controller}/${action}`;


    //#region [async]
    protected async getAsync<T>(action: string, query?: any): Promise<ServiceReturnValue<T>> {    
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

    protected async postAsync<T>(action: string, command: any, useFormData: boolean = false): Promise<ServiceReturnValue<T>> {
        if(useFormData)
            command = this.formDataService.toFormData(command);

        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.post<T>(url, command, {
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

    protected async putAsync<T>(action: string, command: any, useFormData: boolean = false): Promise<ServiceReturnValue<T>> {
        if(useFormData)
            command = this.formDataService.toFormData(command);

        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.put<T>(url, command, {
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

    protected async deleteAsync<T>(action: string, command?: any): Promise<ServiceReturnValue<T>> {
        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.delete<T>(url, {
                    params: command ? new HttpParams({ fromObject: command }) : undefined, 
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

    protected async patchAsync<T>(action: string, command: any, useFormData: boolean = false): Promise<ServiceReturnValue<T>> {
        if(useFormData)
            command = this.formDataService.toFormData(command);

        var url = this.getUrl(action);
        try {
            const result = await lastValueFrom(
                this.httpClient.patch<T>(url, command, {
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
    //#endregion

    //#region [sync] 
    protected get<T>(action: string, query?: any): Observable<ServiceReturnValue<T>> {
        const url = this.getUrl(action);
        return this.httpClient.get<T>(url, {
            params: query ? new HttpParams({ fromObject: query }) : undefined,
            observe: 'response',
        }).pipe(
            map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
            catchError((error: HttpErrorResponse) => {
                const mappedError = this.mapError<T>(error);
                return of(mappedError); // returning an observable of the error
            })
        );
    }

    protected post<T>(action: string, command: any, useFormData: boolean = false): Observable<ServiceReturnValue<T>> {
        if(useFormData)
            command = this.formDataService.toFormData(command);

        const url = this.getUrl(action);
        return this.httpClient.post<T>(url, command, { observe: 'response' }).pipe(
            map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
            catchError((error: HttpErrorResponse) => {
                const mappedError = this.mapError<T>(error);
                return of(mappedError); // returning an observable of the error
            })
        );
    }

    protected put<T>(action: string, command: any, useFormData: boolean = false): Observable<ServiceReturnValue<T>> {
        if(useFormData)
            command = this.formDataService.toFormData(command);

        const url = this.getUrl(action);
        return this.httpClient.put<T>(url, command, { observe: 'response' }).pipe(
            map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
            catchError((error: HttpErrorResponse) => {
                const mappedError = this.mapError<T>(error);
                return of(mappedError); // returning an observable of the error
            })
        );
    }

    protected delete<T>(action: string, command?: any): Observable<ServiceReturnValue<T>> {
        const url = this.getUrl(action);
        return this.httpClient.delete<T>(url, {
            params: command ? new HttpParams({ fromObject: command }) : undefined,
            observe: 'response',
        }).pipe(
            map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
            catchError((error: HttpErrorResponse) => {
                const mappedError = this.mapError<T>(error);
                return of(mappedError); // returning an observable of the error
            })
        );
    }

    protected patch<T>(action: string, command: any, useFormData: boolean = false): Observable<ServiceReturnValue<T>> {
        if(useFormData)
            command = this.formDataService.toFormData(command);

        const url = this.getUrl(action);
        return this.httpClient.patch<T>(url, command, { observe: 'response' }).pipe(
            map((response: HttpResponse<T>) => this.mapResponse<T>(response)),
            catchError((error: HttpErrorResponse) => {
                const mappedError = this.mapError<T>(error);
                return of(mappedError); // returning an observable of the error
            })
        );
    }
    //#endregion

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
            error: error.error ?? 'Unknown error', // fix and parse as error list with variable key
            success: false
        }
    }
}