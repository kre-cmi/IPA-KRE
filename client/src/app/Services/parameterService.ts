import {Injectable} from '@angular/core';
import {HttpService} from './httpService';

@Injectable()
export class NewsService {
	constructor(private _http: HttpService) {
	}

	public async getAllNewsForManagementClient(): Promise<string[]> {
		let url = this._createBaseUrl() + '/GetAllNewsForManagementClient';
		return await this._http.get<string[]>(url, this._http.noCaching).toPromise();
	}

	public async deleteNews(idsToDelete: any): Promise<any> {
		let url = this._createBaseUrl() + '/DeleteNews';
		return await this._http.post(url, idsToDelete, this._http.noCaching).toPromise();
	}

	public async getSingleNews(id: string): Promise<any> {
		let url = this._createBaseUrl() + '/GetSingleNews/' + id;
		return await this._http.get<string>(url, this._http.noCaching).toPromise();
	}

	private _createBaseUrl(): string {
		// return this._options.serverUrl + this._options.publicPort + '/api/News';
		return 'toDo';
	}

	public async insertOrUpdateNews(news: any): Promise<any> {
		let url = this._createBaseUrl() + '/InsertOrUpdateNews';
		return await this._http.post(url, news, this._http.noCaching).toPromise();
	}
}
