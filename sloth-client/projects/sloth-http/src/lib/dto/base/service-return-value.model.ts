import { ErrorModel } from "./error.model";

export interface ServiceReturnValue<T> {
    data: T;
    responseCode: number;
    error: ErrorModel  | null;
    success: boolean;
}