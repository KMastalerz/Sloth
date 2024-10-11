export type GridMetadata = {
    columns?: number;
    rows?: number;
    columnsRatio?: (string | number)[];
    rowsRatio?: (string | number)[];
    gridSpans?: GridSpan[];
};

export type GridSpan = {
    type: 'column' | 'row';
    id: number;
    spanFrom: number;
    spanTo: number;
}