export interface ServiceReturnValue<T> {
    data: T;
    responseCode: number;
    error: string | null;
    success: boolean;
}