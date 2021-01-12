import { Pipe, PipeTransform } from '@angular/core';
import { Photo } from './photo';

@Pipe({
    name: 'filterByDescription'
})
export class FilterByDescriptionPipe implements PipeTransform {

    transform(photos: Photo[], descriptionQuery: string): Photo[] {

        descriptionQuery = descriptionQuery.trim().toLowerCase();

        if (descriptionQuery) {
            return photos.filter(photo => photo.title.toLowerCase().includes(descriptionQuery))
        } else {
            return photos;
        }
    }
}