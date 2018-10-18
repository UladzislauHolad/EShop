export class TableData<T> {
    total: number;
    data: T[];

    constructor(data, total) {
        this.data = data;
        this.total = total;
    }
}