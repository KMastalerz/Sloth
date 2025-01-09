export interface ErrorModel {
    type: string;
    title: string;
    status: number;
    errors: { [key: string]: string[] };
}