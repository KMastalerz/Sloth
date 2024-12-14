export type PageLayoutMetadata = {
    columns?: number;
    columnsRatio?: (string | number)[];
    gridAreas?: GridArea[];
    rows?: number;
    rowsRatio?: (string | number)[];    
};

export type GridArea = {
    id: number;
    spanFrom: number;
    spanTo: number;
    type: 'column' | 'row';    
}

export type LinkMetadata = {
    counterSubject?: string,
    errorCount?: number
    warningCount?: number,
}

export type ButtonMetadata = {
    counterSubject?: string,
    errorCount?: number
    warningCount?: number,
}