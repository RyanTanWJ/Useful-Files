package main

import (
	"encoding/json"
	"fmt"
	"net/http"
	"os"
	"regexp"
	"strings"

	"golang.org/x/net/html"
	"gopkg.in/yaml.v2"
)

type Prefectures struct {
	PrefectureMap map[string][]PrefectureInfo `json:"prefectures" yaml:"prefectures"`
}

type PrefectureInfo struct {
	Name     string `json:"name" yaml:"name"`
	KanaName string `json:"kana_name" yaml:"kana_name"`
	// ZipCodes []string `json:"zip_codes" yaml:"zip_codes"`
}

const (
	japanZipCodesUrl = "http://japanzipcodes.blogspot.com/search/label/"
	endUrl           = "%20ZIP%20codes"
)

func main() {
	prefectures := makeJapanPrefecturesMap()

	outputData := &Prefectures{PrefectureMap: map[string][]PrefectureInfo{}}
	for prefName, prefKanaName := range prefectures {

		// GET Request
		webUrl := fmt.Sprintf("%s%s%s", japanZipCodesUrl, prefName, endUrl)
		resp, err := http.Get(webUrl)
		if err != nil {
			panic(fmt.Errorf("Error making http req, %s", err))
		}

		// Parse the body
		defer resp.Body.Close()
		page := html.NewTokenizer(resp.Body)
		if err != nil {
			panic(fmt.Errorf("Error tokenizing resp body, %s", err))
		}

		for {
			_ = page.Next()
			token := page.Token()
			re := regexp.MustCompile("[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]")
			match := re.FindStringSubmatch(token.Data)
			if len(match) > 0 {

				// Create the struct
				prefInfo := PrefectureInfo{Name: prefName, KanaName: prefKanaName}

				// Parse zip code
				zipCode := match[0]
				zipCode = strings.Replace(zipCode, "-", "", -1)
				// prefInfo.ZipCodes = append(prefInfo.ZipCodes, zipCode)

				// Parse first 2 digits
				first2digits := zipCode[:2]

				if !isDuplicate(prefInfo, outputData.PrefectureMap[first2digits]) {
					outputData.PrefectureMap[first2digits] = append(outputData.PrefectureMap[first2digits], prefInfo)
				}
			}
			if token.Type == html.ErrorToken {
				break
			}
		}
	}

	// JSON output
	jsonOutput, err := json.Marshal(outputData)
	if err != nil {
		panic(fmt.Errorf("Error json marshalling, %s", err))
	}
	jsonFile, err := os.Create("Go/DataScraper/output/japan_zip_codes.json")
	if err != nil {
		panic(fmt.Errorf("Error creating json file, %s", err))
	}
	_, err = jsonFile.Write(jsonOutput)
	if err != nil {
		panic(fmt.Errorf("Error writing to json file, %s", err))
	}

	// YML Output
	yamlOutput, err := yaml.Marshal(outputData)
	if err != nil {
		panic(fmt.Errorf("Error yaml marshalling, %s", err))
	}
	ymlFile, err := os.Create("Go/DataScraper/output/japan_zip_codes.yml")
	if err != nil {
		panic(fmt.Errorf("Error creating yaml file, %s", err))
	}
	_, err = ymlFile.Write(yamlOutput)
	if err != nil {
		panic(fmt.Errorf("Error writing to yaml file, %s", err))
	}
}

func makeJapanPrefecturesMap() map[string]string {
	return map[string]string{
		"Aichi":     "愛知県",
		"Akita":     "秋田県",
		"Aomori":    "青森県",
		"Chiba":     "千葉県",
		"Ehime":     "愛媛県",
		"Fukui":     "福井県",
		"Fukuoka":   "福岡県",
		"Fukushima": "福島県",
		"Gifu":      "岐阜県",
		"Gunma":     "群馬県",
		"Hiroshima": "広島県",
		"Hokkaido":  "北海道",
		"Hyogo":     "兵庫県",
		"Ibaraki":   "茨城県",
		"Ishikawa":  "石川県",
		"Iwate":     "岩手県",
		"Kagawa":    "香川県",
		"Kagoshima": "鹿児島県",
		"Kanagawa":  "神奈川県",
		"Kochi":     "高知県",
		"Kunamoto":  "熊本県",
		"Kyoto":     "京都府",
		"Mie":       "三重県",
		"Miyagi":    "宮城県",
		"Miyazaki":  "宮崎県",
		"Nagano":    "長野県",
		"Nagasaki":  "長崎県",
		"Nara":      "奈良県",
		"Niigata":   "新潟県",
		"Oita":      "大分県",
		"Okayama":   "岡山県",
		"Okinawa":   "沖縄県",
		"Osaka":     "大阪府",
		"Saga":      "佐賀県",
		"Saitama":   "埼玉県",
		"Shiga":     "滋賀県",
		"Shimane":   "島根県",
		"Shizuoka":  "静岡県",
		"Tochigi":   "栃木県",
		"Tokushima": "徳島県",
		"Tokyo":     "東京都",
		"Tottori":   "鳥取県",
		"Toyama":    "富山県",
		"Wakayama":  "和歌山県",
		"Yamagata":  "山形県",
		"Yamaguchi": "山口県",
		"Yamanashi": "山梨県",
	}
}

func isDuplicate(prefInfo PrefectureInfo, infoList []PrefectureInfo) bool {
	for _, existingInfo := range infoList {
		if prefInfo == existingInfo {
			return true
		}
	}
	return false
}
