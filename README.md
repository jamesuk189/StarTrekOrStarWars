# Star Trek or Star Wars

A trial of Microsoft's automated ML.NET in a text classification task.

It was trained using Star Trek and Star Wars scripts from Kaggle.

## Training the model

A pre-trained model is provided in the `api` directory named `Model.zip`.

The model was trained for 10 minutes with the following command.

`mlnet classification --dataset "dataset.csv" --label-col Label --has-header true --train-time 600`

## Running Locally

Run the following command from `src`.  

`npm run build`  

This will copy the appropriate HTML, CSS and JavaScript files to the `src/dist` directory.

Run the following command from the root of the project.  

`npx swa start --app-location src/dist --api-location api`