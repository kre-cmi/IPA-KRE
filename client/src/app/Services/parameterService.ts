import {Injectable} from '@angular/core';
import {HttpService} from './httpService';
import {Parameter} from '../parameterManager/parameterEntity';

@Injectable()
export class ParameterService {
	constructor(private _http: HttpService) {
	}
	public async getAllParameters() {
		let url = this._createBaseUrl() + '/GetAllParameters';
		return await this._http.get<Parameter[]>(url, this._http.noCaching).toPromise();
	}
	public async saveParameter(param: Parameter) {
		let url = this._createBaseUrl() + '/SaveParameter' + param;
		return await this._http.post<boolean>(url, this._http.noCaching).toPromise();
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
		let loc = window.location;
		let port = isNaN(parseInt(loc.port, 10)) ? undefined : parseInt(loc.port, 10);
		let baseUrl = '' + loc.protocol + '//' + loc.hostname + (port ? ':' + port : '') + '/ipa/Controllers';
		console.log(baseUrl);
		return baseUrl;
	}

	public async insertOrUpdateNews(news: any): Promise<any> {
		let url = this._createBaseUrl() + '/InsertOrUpdateNews';
		return await this._http.post(url, news, this._http.noCaching).toPromise();
	}
}
